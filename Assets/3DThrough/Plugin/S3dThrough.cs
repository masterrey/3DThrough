using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Mouse3D
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Editor window para controlar a câmera do Unity Scene View e objetos selecionados usando um mouse 3D (3DConnexion SpaceMouse).
    /// Fornece integração com dispositivos 3D para navegação intuitiva no editor.
    /// </summary>
    public class S3dThrough : EditorWindow
    {
        #region Campos Privados
        
        bool groupEnabled;
        bool onetimeb = true;
        
        /// <summary>
        /// Quando ativado, expõe os valores brutos de translação e rotação através da classe E3dThrough.
        /// </summary>
        public bool bruteAxis = false;
        
        /// <summary>Sensor do dispositivo 3D para capturar movimentos de translação.</summary>
        protected TDx.TDxInput.Sensor sensor;
        
        /// <summary>Teclado do dispositivo 3D para capturar pressionamentos de botões.</summary>
        protected TDx.TDxInput.Keyboard keyboard;
        
        /// <summary>Instância do dispositivo 3D conectado.</summary>
        protected TDx.TDxInput.Device device;
        
        private bool isRunning;
        
        /// <summary>Indica se houve uma falha na inicialização do dispositivo 3D.</summary>
        private bool deviceInitFailed = false;
        
        /// <summary>Quando ativado, controla o objeto selecionado em vez da câmera do Scene View.</summary>
        private bool objectfocus;
        
        /// <summary>Posição atual da câmera ou do objeto sendo controlado.</summary>
        Vector3 posicao;
        
        /// <summary>Rotação atual em ângulos de Euler da câmera.</summary>
        Vector3 rotacao;
        
        /// <summary>Sensibilidade da rotação (0.0 a 1.0).</summary>
        float rotationsensivity = 0.1f;
        
        /// <summary>Sensibilidade da translação (0.0 a 1.0).</summary>
        float translationsensivity = 0.1f;
        
        float distsensivty = 0.1f;
        
        /// <summary>Referência ao Transform do objeto atualmente selecionado.</summary>
        Transform thist;
        
        /// <summary>Referência estática à janela do editor.</summary>
        static S3dThrough window;
        
        /// <summary>Flags para controle de toggle dos botões do dispositivo 3D.</summary>
        bool toglebutton1, toglebutton2, toglebutton3;
        
        #endregion

        #region Menu Item e Inicialização
        
        /// <summary>
        /// Abre a janela do editor 3D Through através do menu Window/3D Through.
        /// </summary>
        [MenuItem("Tools/3D Through")]
        static void Init()
        {
            // Obtém janela existente ou cria uma nova
            window = GetWindow<S3dThrough>();
        }
        
        #endregion

        #region Métodos do Ciclo de Vida do Editor
        
        /// <summary>
        /// Chamado quando a janela é destruída. Desconecta o dispositivo 3D.
        /// </summary>
        void OnDestroy()
        {
            DisconnectDevice();
        }
        
        /// <summary>
        /// Chamado quando a janela é desabilitada. Salva preferências e desconecta o dispositivo.
        /// </summary>
        void OnDisable()
        {
            EditorPrefs.SetFloat("m3dTrotationsensivity", rotationsensivity);
            EditorPrefs.SetFloat("m3dTtranslationsensivity", translationsensivity);
            DisconnectDevice();
        }
        
        /// <summary>
        /// Desconecta o dispositivo 3D de forma segura.
        /// </summary>
        void DisconnectDevice()
        {
            try
            {
                if (device != null && device.IsConnected)
                {
                    this.device.Disconnect();
                    Debug.Log("3DConnexion device disconnected.");
                }
            }
            catch (COMException e)
            {
                Debug.LogWarning("Error disconnecting 3DConnexion device: " + e.Message);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Unexpected error disconnecting device: " + e.Message);
            }
        }
        
        /// <summary>
        /// Chamado quando a janela é habilitada. Carrega preferências salvas.
        /// </summary>
        void OnEnable()
        {
            rotationsensivity = EditorPrefs.GetFloat("m3dTrotationsensivity", 0.1f);
            translationsensivity = EditorPrefs.GetFloat("m3dTtranslationsensivity", 0.1f);
        }
        
        #endregion

        #region Inicialização do Dispositivo
        
        /// <summary>
        /// Inicializa e conecta o dispositivo 3D se ainda não estiver conectado.
        /// </summary>
        void init()
        {
            // Se já falhou anteriormente, não tenta novamente
            if (deviceInitFailed)
                return;
                
            try
            {
                if (this.device == null)
                {
                    this.device = new TDx.TDxInput.Device();
                    sensor = this.device.Sensor;
                    keyboard = this.device.Keyboard;
                    this.device.LoadPreferences("Unity");
                }

                // Conecta o dispositivo se não estiver conectado
                if (!device.IsConnected)
                {
                    this.device.Connect();
                    Debug.Log("3DConnexion device connected successfully!");
                }
            }
            catch (COMException e)
            {
                deviceInitFailed = true;
                Debug.LogWarning("3DConnexion device could not be initialized. Please ensure:\n" +
                    "1. 3DConnexion drivers are installed\n" +
                    "2. A 3D mouse is connected\n" +
                    "3. The device is properly registered\n\n" +
                    "Error details: " + e.Message);
            }
            catch (Exception e)
            {
                deviceInitFailed = true;
                Debug.LogWarning("Unexpected error initializing 3DConnexion device: " + e.Message);
            }
        }
        
        /// <summary>
        /// Inicialização única para capturar o estado inicial da câmera do Scene View.
        /// </summary>
        void onetime()
        {
            if (onetimeb)
            {
                onetimeb = false;
                if (SceneView.currentDrawingSceneView != null)
                {
                    posicao = SceneView.currentDrawingSceneView.pivot;
                    rotacao = SceneView.currentDrawingSceneView.rotation.eulerAngles;
                }
            }
        }
        
        #endregion

        #region Métodos de Atualização
        
        /// <summary>
        /// Atualiza a janela a cada frame. Inicializa o dispositivo e processa entrada do sensor.
        /// </summary>
        void Update()
        {
            onetime();
            init();
            if (device != null && device.IsConnected)
            {
                this.sensor_SensorInput();
            }
        }
        
        #endregion

        #region Interface GUI
        
        /// <summary>
        /// Desenha a interface gráfica da janela do editor.
        /// Exibe controles para alternar entre modo objeto/câmera e ajustar sensibilidades.
        /// </summary>
        void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            
            // Exibe status do dispositivo
            if (deviceInitFailed)
            {
                EditorGUILayout.HelpBox("3DConnexion device not available. Please check:\n" +
                    "• 3DConnexion drivers are installed\n" +
                    "• Device is connected\n" +
                    "• Device is properly registered", MessageType.Warning);
                    
                if (GUILayout.Button("Retry Connection"))
                {
                    deviceInitFailed = false;
                    device = null;
                    init();
                }
            }
            else if (device != null && device.IsConnected)
            {
                EditorGUILayout.HelpBox("3DConnexion device connected!", MessageType.Info);
            }
            
            EditorGUILayout.Space();
            
            objectfocus = EditorGUILayout.Toggle("Object", objectfocus);
            rotationsensivity = EditorGUILayout.Slider("Rot sens", rotationsensivity, 0, 1);
            translationsensivity = EditorGUILayout.Slider("Trans sens", translationsensivity, 0, 1);
            bruteAxis = EditorGUILayout.Toggle("Brute Axis", bruteAxis);
        }
        
        #endregion

        #region Processamento de Entrada do Sensor
        
        /// <summary>
        /// Processa a entrada do sensor 3D e atualiza a câmera do Scene View ou o objeto selecionado.
        /// Suporta translação, rotação e vários atalhos de botão para funcionalidades rápidas.
        /// 
        /// Atalhos de Botão:
        /// - Botão 2: Toggle entre modo objeto e câmera
        /// - Botão 3: Reset da câmera
        /// - Botão 4: Toggle entre perspectiva e ortográfico
        /// - Botão 5: Toggle do modo Brute Axis
        /// - Botões 7-11: Visualizações predefinidas (cima, esquerda, direita, frente, padrão)
        /// </summary>
        void sensor_SensorInput()
        {
            if (Selection.activeTransform)
            {
                thist = Selection.activeTransform;
            }
            else
            {
                objectfocus = false;
            }
            
            // Captura dados de translação e rotação do sensor
            TDx.TDxInput.Vector3D translation = sensor.Translation;
            TDx.TDxInput.AngleAxis rotation = sensor.Rotation;
            
            if (SceneView.currentDrawingSceneView != null)
            {
                // Botão 2: Toggle entre modo objeto e câmera
                if (keyboard.IsKeyDown(2) && toglebutton1)
                {
                    objectfocus = !objectfocus;
                    toglebutton1 = false;
                }
                if (keyboard.IsKeyUp(2))
                {
                    toglebutton1 = true;
                }
                
                // Botão 3: Reset da câmera
                if (keyboard.IsKeyDown(3))
                {
                    SceneView.currentDrawingSceneView.LookAt(
                        SceneView.currentDrawingSceneView.camera.transform.position,
                        SceneView.currentDrawingSceneView.camera.transform.rotation, 1);
                }
                
                // Botão 4: Toggle entre perspectiva e ortográfico
                if (keyboard.IsKeyDown(4) && toglebutton2)
                {
                    toglebutton2 = false;
                    SceneView.currentDrawingSceneView.orthographic = !SceneView.currentDrawingSceneView.orthographic;
                }
                if (keyboard.IsKeyUp(4))
                {
                    toglebutton2 = true;
                }
                
                // Botão 5: Toggle do modo Brute Axis
                if (keyboard.IsKeyDown(5) && toglebutton3)
                {
                    toglebutton3 = false;
                    bruteAxis = !bruteAxis;
                }
                if (keyboard.IsKeyUp(5))
                {
                    toglebutton3 = true;
                }
                
                // Botões 7-11: Visualizações predefinidas para o objeto selecionado
                if (keyboard.IsKeyDown(7))
                {
                    if (Selection.activeTransform)
                    {
                        thist = Selection.activeTransform;
                        SceneView.currentDrawingSceneView.LookAt(thist.position, Quaternion.LookRotation(Vector3.down));
                    }
                }
                if (keyboard.IsKeyDown(8))
                {
                    if (Selection.activeTransform)
                    {
                        thist = Selection.activeTransform;
                        SceneView.currentDrawingSceneView.LookAt(thist.position, Quaternion.LookRotation(Vector3.left));
                    }
                }
                if (keyboard.IsKeyDown(9))
                {
                    if (Selection.activeTransform)
                    {
                        thist = Selection.activeTransform;
                        SceneView.currentDrawingSceneView.LookAt(thist.position, Quaternion.LookRotation(Vector3.right));
                    }
                }
                if (keyboard.IsKeyDown(10))
                {
                    if (Selection.activeTransform)
                    {
                        thist = Selection.activeTransform;
                        SceneView.currentDrawingSceneView.LookAt(thist.position, Quaternion.LookRotation(Vector3.forward));
                    }
                }
                if (keyboard.IsKeyDown(11))
                {
                    if (Selection.activeTransform)
                    {
                        thist = Selection.activeTransform;
                        SceneView.currentDrawingSceneView.LookAt(thist.position);
                    }
                }
                
                // Processar entrada do sensor quando há movimento
                if (translation.Length > 0 || rotation.Angle > 0)
                {
                    // Modo Brute Axis: expõe valores brutos através de E3dThrough
                    if (bruteAxis)
                    {
                        E3dThrough.BruteTranslation = new Vector3(
                            -(float)translation.X,
                            (float)translation.Y,
                            -(float)translation.Z);
                        E3dThrough.BruteRotation = new Vector3(
                            (float)rotation.X,
                            (float)rotation.Y,
                            -(float)rotation.Z) * (float)rotation.Angle * -rotationsensivity / 10;
                        return;
                    }
                    
                    // Aplicar translação relativa à câmera
                    posicao += SceneView.currentDrawingSceneView.camera.transform.forward * -(float)translation.Z * translationsensivity;
                    posicao += SceneView.currentDrawingSceneView.camera.transform.TransformDirection(Vector3.left) * -(float)translation.X * translationsensivity;
                    posicao += SceneView.currentDrawingSceneView.camera.transform.TransformDirection(Vector3.up) * (float)translation.Y * translationsensivity;

                    // Aplicar rotação
                    rotacao += new Vector3((float)rotation.X, (float)rotation.Y, -(float)rotation.Z) * (float)rotation.Angle * -rotationsensivity / 10;
                    
                    // Modo Object Focus: controla o objeto selecionado
                    if (objectfocus)
                    {
                        if (Selection.activeTransform)
                        {
                            thist.position = posicao;
                            thist.RotateAround(SceneView.currentDrawingSceneView.camera.transform.forward, 
                                (float)rotation.Angle * (float)rotation.Z / 1000);
                            thist.RotateAround(SceneView.currentDrawingSceneView.camera.transform.TransformDirection(Vector3.left), 
                                (float)rotation.Angle * (float)rotation.X / 1000);
                            thist.RotateAround(SceneView.currentDrawingSceneView.camera.transform.TransformDirection(Vector3.up), 
                                (float)rotation.Angle * -(float)rotation.Y / 1000);
                            SceneView.currentDrawingSceneView.LookAt(posicao);
                            SceneView.currentDrawingSceneView.Focus();
                        }
                    }
                    // Modo Camera: controla a câmera do Scene View
                    else
                    {
                        SceneView.currentDrawingSceneView.LookAt(posicao);
                        SceneView.currentDrawingSceneView.rotation = Quaternion.Euler(rotacao);
                        SceneView.currentDrawingSceneView.Focus();
                    }
                }
                // Sem movimento: sincroniza posição com câmera ou objeto
                else
                {
                    if (!objectfocus)
                    {
                        posicao = SceneView.currentDrawingSceneView.pivot;
                        rotacao = SceneView.currentDrawingSceneView.rotation.eulerAngles;
                    }
                    else
                    {
                        if (Selection.activeTransform)
                        {
                            posicao = thist.position;
                            Undo.RecordObject(thist, "Move " + thist.name);
                        }
                    }
                }
            }
        }
        
        #endregion
    }
}

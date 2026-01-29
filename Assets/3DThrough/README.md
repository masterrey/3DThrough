# 3D Through - Plugin Unity para Mouse 3D

Plugin Unity Editor para integração com dispositivos 3D (3DConnexion SpaceMouse e similares).
Permite controlar a câmera do Scene View e manipular objetos selecionados usando entrada 6DOF (6 Degrees of Freedom).

## Requisitos

- Unity Editor (testado em versões antigas)
- Dispositivo 3D compatível (3DConnexion SpaceMouse, Space Navigator, etc.)
- Driver TDxInput instalado e configurado

## Instalação

1. Copie a pasta `3DThrough` para `Assets/` do seu projeto Unity
2. Certifique-se de que o driver do dispositivo 3D está instalado
3. Abra a janela através do menu: **Window > 3D Through**

## Uso

### Modos de Operação

#### Modo Câmera (padrão)
- Move e rotaciona a câmera do Scene View
- Use o dispositivo 3D para navegar livremente pela cena

#### Modo Objeto
- Ative através do toggle "Object" na janela do plugin (ou Botão 2 do dispositivo)
- Controla diretamente o objeto selecionado na hierarquia
- Útil para posicionar e rotacionar objetos com precisão

#### Modo Brute Axis
- Ative através do toggle "Brute Axis" na janela (ou Botão 5 do dispositivo)
- Expõe valores brutos do sensor através de `E3dThrough.BruteTranslation` e `E3dThrough.BruteRotation`
- Útil para implementações customizadas

### Controles da Interface

- **Object**: Toggle entre modo câmera e modo objeto
- **Rot sens**: Sensibilidade da rotação (0.0 - 1.0)
- **Trans sens**: Sensibilidade da translação (0.0 - 1.0)
- **Brute Axis**: Toggle do modo de valores brutos

### Atalhos do Dispositivo 3D

| Botão | Função |
|-------|--------|
| 2 | Toggle modo Objeto/Câmera |
| 3 | Reset da câmera |
| 4 | Toggle Perspectiva/Ortográfico |
| 5 | Toggle modo Brute Axis |
| 7 | Vista de cima (objeto selecionado) |
| 8 | Vista esquerda (objeto selecionado) |
| 9 | Vista direita (objeto selecionado) |
| 10 | Vista frontal (objeto selecionado) |
| 11 | Vista padrão (objeto selecionado) |

## Estrutura do Código

### S3dThrough.cs
Classe principal do Editor Window. Gerencia:
- Conexão com o dispositivo 3D
- Processamento de entrada do sensor
- Controle da câmera e objetos
- Interface do usuário

### E3dThrough.cs
Classe utilitária estática para expor valores brutos do sensor quando o modo Brute Axis está ativo.

## Atualizações na Versão Moderna

Esta versão foi modernizada com:
- Uso de `EditorPrefs` em vez de `PlayerPrefs` (apropriado para configurações do editor)
- Uso de `GetWindow<T>()` em vez de `GetWindow(typeof(T))` (sintaxe moderna)
- Uso de `Undo.RecordObject` em vez de `Undo.RegisterUndo` (API atualizada)
- Documentação XML completa em português
- Código organizado com regions
- Remoção de código comentado não utilizado
- Formatação e indentação consistentes

## Notas Técnicas

- As preferências são salvas em `EditorPrefs` com chaves:
  - `m3dTrotationsensivity`: Sensibilidade de rotação
  - `m3dTtranslationsensivity`: Sensibilidade de translação
  
- O dispositivo é automaticamente reconectado a cada frame se desconectado

- Movimentos são aplicados no espaço relativo à câmera do Scene View

## Limitações Conhecidas

- Requer driver TDxInput COM instalado
- Testado apenas com dispositivos 3DConnexion
- Alguns valores de rotação podem precisar de ajuste fino dependendo do dispositivo

## Licença

Este é um plugin antigo adaptado e modernizado. Use conforme necessário em seus projetos Unity.

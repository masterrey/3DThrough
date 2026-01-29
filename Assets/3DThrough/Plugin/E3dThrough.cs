using UnityEngine;
using System.Collections;

/// <summary>
/// Classe estática para expor valores brutos de translação e rotação do mouse 3D.
/// Usada quando o modo "Brute Axis" está ativado no S3dThrough para acesso direto aos dados do sensor.
/// </summary>
public class E3dThrough
{
    /// <summary>
    /// Vetor de translação bruto do dispositivo 3D.
    /// Valores representam movimento nos eixos X, Y e Z sem processamento adicional.
    /// </summary>
    public static Vector3 BruteTranslation;
    
    /// <summary>
    /// Vetor de rotação bruto do dispositivo 3D com sensibilidade aplicada.
    /// Valores representam rotação nos eixos X, Y e Z multiplicados pela sensibilidade configurada.
    /// </summary>
    public static Vector3 BruteRotation;
}

using UnityEngine;
using System.Collections;

namespace Mouse3D
{
    /// <summary>
    /// Static class to expose raw translation and rotation values from the 3D mouse.
    /// Used when "Brute Axis" mode is enabled in S3dThrough for direct access to sensor data.
    /// </summary>
    public class E3dThrough
    {
        /// <summary>
        /// Raw translation vector from the 3D device.
        /// Values represent movement on X, Y and Z axes without additional processing.
        /// </summary>
        public static Vector3 BruteTranslation;
        
        /// <summary>
        /// Raw rotation vector from the 3D device with sensitivity applied.
        /// Values represent rotation on X, Y and Z axes multiplied by the configured sensitivity.
        /// </summary>
        public static Vector3 BruteRotation;
    }
}

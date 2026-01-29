using UnityEngine;
using System.Collections;

namespace Mouse3D
{
	/// <summary>
	/// Example script that demonstrates how to read brute axis values from the 3D mouse.
	/// Prints the translation values to the console for debugging purposes.
	/// </summary>
	public class Movecamera : MonoBehaviour
	{
		// Use this for initialization
		void Start()
		{
		
		}
		
		// Update is called once per frame
		void Update()
		{
			// Print 3D mouse translation values for debugging
			print(E3dThrough.BruteTranslation);
		}
	}
}

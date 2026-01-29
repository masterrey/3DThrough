using UnityEngine;
using System.Collections;

namespace Mouse3D
{
	/// <summary>
	/// Example script that demonstrates how to use brute axis values from the 3D mouse.
	/// Moves the GameObject continuously based on the 3D mouse translation input.
	/// </summary>
	public class MoveThough : MonoBehaviour
	{
		// Use this for initialization
		void Start()
		{
		
		}
		
		// Update is called once per frame
		void Update()
		{
			// Move the object based on 3D mouse input
			transform.position += E3dThrough.BruteTranslation;
		}
	}
}

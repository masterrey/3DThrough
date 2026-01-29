using UnityEngine;
using System.Collections;

public class MoveThough : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position+=E3dThrough.BruteTranslation;
		
	}
}

using UnityEngine;
using System.Collections;

public class ProjectileFired : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {

	}
	
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "house") {
			Destroy (col.gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class ProjectileFired : MonoBehaviour {

	public static int housesDestroyed = 0;

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "house") {
			Destroy (col.gameObject);
			++housesDestroyed;
		}
	}

	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}

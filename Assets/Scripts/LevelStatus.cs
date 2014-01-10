using UnityEngine;
using System.Collections;

public class LevelStatus : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {
		GetComponent<TextMesh>().text = "win";
	}
}

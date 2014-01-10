using UnityEngine;
using System.Collections;

public class LevelTime : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		GetComponent<TextMesh>().text = "win";
	}
}

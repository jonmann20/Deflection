using UnityEngine;
using System.Collections;

public class GameCta : MonoBehaviour {

    const KeyCode key = KeyCode.Return;

	void Start () {
		
	}

	void Update () {
		if(Input.GetKey(key)){
			Application.LoadLevel("main");
		}
	}
}

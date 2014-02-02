using UnityEngine;
using System.Collections;

public class TextCtaController : MonoBehaviour {

	public KeyCode key = KeyCode.Return;
	
	void Update () {
		if(Input.GetKey(key)){
			Application.LoadLevel("main");
		}
	}
}

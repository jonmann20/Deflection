#pragma strict

var key : KeyCode;

function Update () {
	if(Input.GetKey(key)){
		Application.LoadLevel("main");
	}
}
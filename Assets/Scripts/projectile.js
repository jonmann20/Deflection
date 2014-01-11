#pragma strict

var moveUp : KeyCode;
var moveDown : KeyCode;
var spacebar : KeyCode;

var dtY : int = 6;

function Update () {
	// movement
	if(Input.GetKey(moveUp)){
		// move position
		rigidbody2D.angularVelocity = dtY;
	}
	else if(Input.GetKey(moveDown)){
		// move down
		rigidbody2D.angularVelocity = -dtY;
	}
	else {
		rigidbody2D.angularVelocity = 0;
	}
	
	// shoot
	if(Input.GetKey(spacebar)){
		fire();
	}
}

function fire(){
	
}
using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

    const int dtY = 10;
	const KeyCode moveUp = KeyCode.W,
	              moveDown = KeyCode.S,
	              spacebar = KeyCode.Space;

    public GameObject bullet;
    GameObject projectile;

	void Start () {
	
	}

	void Update () {
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

	void fire(){
        projectile = (GameObject)Instantiate(bullet);
	}
}

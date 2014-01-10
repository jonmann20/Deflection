using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	const int dtY = 15;
	
	public Rigidbody2D pfBullet;

	
	void Start () {
		
	}
	
	void Update () {
		// movement
		if(Input.GetKey(KeyCode.W)){
			// move position
			rigidbody2D.angularVelocity = dtY;
		}
		else if(Input.GetKey(KeyCode.S)){
			// move down
			rigidbody2D.angularVelocity = -dtY;
		}
		else {
			rigidbody2D.angularVelocity = 0;
		}

		// shoot
		if(Input.GetKeyDown(KeyCode.Space)){
			fireProjectile();
		}
	}
	
	void fireProjectile(){
		Transform gun = GetComponent<Transform> ();
		Rigidbody2D projectile = (Rigidbody2D)Instantiate(pfBullet, new Vector2 (gun.position.x, gun.position.y), Quaternion.identity);

		projectile.velocity = new Vector2(4, 3);
	}
}

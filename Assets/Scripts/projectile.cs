using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	const int dtTheta = 2;
	int theta = 0;
	Vector3 initPos;
	public Rigidbody2D pfBullet;


	void Start () {
		initPos = transform.position;
	}
	
	void Update () {
		// movement
		if(Input.GetKey(KeyCode.W)){			// up
			if(theta < 38){
				transform.RotateAround(
					new Vector3(initPos.x - renderer.bounds.size.x/2, initPos.y),
					Vector3.forward,
					dtTheta
				);

				theta += dtTheta;
			}
		}
		else if(Input.GetKey(KeyCode.S)){		// down
			if(theta > -38){
				transform.RotateAround(
					new Vector3(initPos.x - renderer.bounds.size.x/2, initPos.y),
					Vector3.forward,
					-dtTheta
				);

				theta -= dtTheta;
			}
		}

		// shoot
		if(Input.GetKeyDown(KeyCode.Space)){
			fireProjectile();
		}
	}
	
	void fireProjectile(){
		Rigidbody2D projectile = (Rigidbody2D)Instantiate(pfBullet, new Vector2 (transform.position.x, transform.position.y), Quaternion.identity);

		float rad = theta * Mathf.Deg2Rad;
		float y = 4 * Mathf.Tan (rad);

		projectile.velocity = new Vector2(4, y);
	}
}

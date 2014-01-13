using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public Rigidbody2D pfProjectile;
	public bool isPlayer;

	const int DEGREE_RANGE = 38;
	const int DT_THETA = 2;
	int SPEED = -4;
	float theta = 0, dtMove = 0;
	
	//Object pfProjectile = Resources.Load("pfProjectile");
	Vector3 initPos;
	float halfW;


	void Start(){
		initPos = transform.position;

		halfW = renderer.bounds.size.x/2;
		if(isPlayer){
			halfW *= -1;
			SPEED *= -1;
		}
	}

	public void moveGun(Dir dir){
		if(dir == Dir.UP){
			if(theta > DEGREE_RANGE){
				return;
			}

			dtMove = DT_THETA;
		}
		else {
			if(theta < -DEGREE_RANGE){
				return;
			}

			dtMove = -DT_THETA;
		}

		transform.RotateAround(
			new Vector3(initPos.x + halfW, initPos.y),
			Vector3.forward,
			dtMove
		);

		theta += dtMove;
	}

	public void fireProjectile(){
		Rigidbody2D projectile = (Rigidbody2D)Instantiate(pfProjectile, new Vector2 (transform.position.x, transform.position.y), Quaternion.identity);

		float rad = theta * Mathf.Deg2Rad;
		float y = SPEED * Mathf.Tan (rad);
		
		projectile.velocity = new Vector2(SPEED, y);
	}
}

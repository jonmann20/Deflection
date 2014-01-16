using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public Rigidbody pfProjectile;
	public bool isPlayer;

	const int DEGREE_RANGE = 38;
	const int DT_THETA = 2;
	int SPEED = -1800;
	float theta = 0, dtMove = 0;

	Vector3 initPos;
	float halfW, halfH;


	void Start(){
		initPos = transform.position;

		halfW = renderer.bounds.size.x/2;
		halfH = renderer.bounds.size.y/2;
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
			new Vector3(initPos.x + halfW, initPos.y - halfH),
			Vector3.forward,
			dtMove
		);

		theta += dtMove;
	}

	public void fireProjectile(){
		Rigidbody projectile = (Rigidbody)Instantiate(
			pfProjectile,
			new Vector3(transform.position.x + renderer.bounds.size.x, transform.position.y + renderer.bounds.size.y, transform.position.z), 
		    Quaternion.identity
		);

		float rad = theta * Mathf.Deg2Rad;
		float y = SPEED * Mathf.Tan(rad);

		projectile.AddForce(new Vector3(SPEED, y, 0));
	}
}

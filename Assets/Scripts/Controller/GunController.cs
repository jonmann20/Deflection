using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public Rigidbody pfProjectile;
	public bool isPlayer;
	public static bool allowFire = true;

	Vector3 initPos;
	float halfW;

	const int DT_THETA = 2;
	int minDeg, maxDeg;
	float SPEED = -140f;
	float dtMove = 0;


	void Start(){
		initPos = transform.position;
		halfW = renderer.bounds.size.x/2;

		if(isPlayer){
			halfW *= -1;
			SPEED *= -1;

			minDeg = 210;
			maxDeg = 261;
		}
		else {
			minDeg = 261;
			maxDeg = 321;
		}
	}

	public void moveGun(Dir dir){
		dtMove = (dir == Dir.UP) ? DT_THETA : -DT_THETA;

		if(transform.eulerAngles.z + dtMove > minDeg && transform.eulerAngles.z + dtMove < maxDeg){
			transform.RotateAround(
				new Vector3(initPos.x + halfW, initPos.y - renderer.bounds.size.y/2),
				Vector3.forward,
				dtMove
			);
		}
	}

	float theta2y(){
		return SPEED * Mathf.Tan(transform.eulerAngles.z * Mathf.Deg2Rad);
	}

	public void fireProjectile(){
		if(!GunController.allowFire){
			return;
		}

		GunController.allowFire = false;

		Vector3 pos = new Vector3(transform.position.x - halfW*2, transform.position.y + renderer.bounds.size.y, transform.position.z);
		Rigidbody projectile = Instantiate(pfProjectile, pos, transform.rotation) as Rigidbody;

		float y = theta2y();

		projectile.velocity = new Vector3(SPEED, y, 0);
	}
}

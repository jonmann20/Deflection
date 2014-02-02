using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public bool isPlayer;

	Rigidbody bulletPrefab;

	GunController controller;
	
	Vector3 initPos;
	float halfW;
	
	const int DT_THETA = 1;
	int minDeg, maxDeg;
	float BULLET_SPEED = -140f;
	float dtMove = 0;
	

	void Awake(){
		bulletPrefab = Resources.Load<Rigidbody>("Bullet");

		if(isPlayer){
			controller = new PlayerController(this);
		}
		else {
			controller = new OpponentController(this);
		}
	}

	void Start(){
		initPos = transform.position;
		halfW = renderer.bounds.size.x/2;
		
		if(isPlayer){
			halfW *= -1;
			BULLET_SPEED *= -1;
			
			minDeg = 210;
			maxDeg = 261;
		}
		else {
			minDeg = 261;
			maxDeg = 321;
		}
	}

	void Update(){
		controller.CheckInput();
	}

	void OnGUI(){
		GUI.Box(new Rect(20, 20, 100, 100), "Angle");
		GUI.Label(new Rect(50, 50, 100, 100), transform.eulerAngles.z.ToString());
	}

	#region Actions
	public void move(Dir dir){
		dtMove = (dir == Dir.UP) ? DT_THETA : -DT_THETA;
		
		if(transform.eulerAngles.z + dtMove > minDeg && transform.eulerAngles.z + dtMove < maxDeg){
			transform.RotateAround(
				new Vector3(initPos.x + halfW, initPos.y - renderer.bounds.size.y/2),
				Vector3.forward,
				dtMove
			);
		}
	}

	public void shoot(){
		Vector3 pos = new Vector3(transform.position.x - halfW*2, transform.position.y + renderer.bounds.size.y, transform.position.z);
		Rigidbody projectile = Instantiate(bulletPrefab, pos, transform.rotation) as Rigidbody;

		Bullet b = projectile.GetComponent<Bullet>();
		b.init (isPlayer);

		float y = theta2y();
		
		projectile.velocity = new Vector3(BULLET_SPEED, y, 0);

		BattleController.that.endTurn();
	}
	#endregion Actions


	float theta2y(){
		return BULLET_SPEED * Mathf.Tan(transform.eulerAngles.z * Mathf.Deg2Rad);
	}
	
}

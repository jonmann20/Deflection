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
	
	string angleStr;
	float angle;

	void Awake(){
		bulletPrefab = Resources.Load<Rigidbody>("Bullet");

		if(isPlayer){
			controller = gameObject.AddComponent<PlayerController>();
		}
		else {
			controller = gameObject.AddComponent<OpponentController>();
		}

		angleStr = transform.eulerAngles.z.ToString();
		angle = transform.eulerAngles.z;
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
		// current angle
		GUI.Box(new Rect(3, 3, 160, 23), "Current angle: " + transform.eulerAngles.z.ToString());

		// new angle
		angleStr = GUI.TextField(new Rect(3, 30, 30, 20), angleStr, 3);

		if (GUI.Button (new Rect (35, 30, 130, 20), "Click to set angle")) {
			if(float.TryParse(angleStr, out angle)){
				float newAngle = angle - transform.eulerAngles.z;
				rotateTo(newAngle);
			}
			else {
				// GUI.Label("not a #");
			}
		}
	}

	#region Actions
	public void move(Dir dir){
		dtMove = (dir == Dir.UP) ? DT_THETA : -DT_THETA;
		rotateTo(dtMove);
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

	void rotateTo(float dtAngle){
		if(transform.eulerAngles.z + dtAngle > minDeg && transform.eulerAngles.z + dtAngle < maxDeg){
			transform.RotateAround(
				new Vector3(initPos.x + halfW, initPos.y - renderer.bounds.size.y/2),
				Vector3.forward,
				dtAngle
			);
		}
		else {
			// GUI.Label("outside of range");
		}
	}

	float theta2y(){
		return BULLET_SPEED * Mathf.Tan(transform.eulerAngles.z * Mathf.Deg2Rad);
	}
	
}

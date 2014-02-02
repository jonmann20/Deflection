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
	float dtMove = 0;

	string angleStr, speedStr;
	float angle, bulletSpeed, normalizedSpeed = 140;

	void Awake(){
		bulletPrefab = Resources.Load<Rigidbody>("Bullet");

		if(isPlayer){
			controller = gameObject.AddComponent<PlayerController>();
		}
		else {
			controller = gameObject.AddComponent<OpponentController>();
		}
	}

	void Start(){
		initPos = transform.position;
		halfW = renderer.bounds.size.x/2;

		angleStr = transform.eulerAngles.z.ToString();
		angle = transform.eulerAngles.z;
		
		speedStr = normalizedSpeed.ToString();
		bulletSpeed = -normalizedSpeed;

		if(isPlayer){
			halfW *= -1;
			bulletSpeed *= -1;
			
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

	#region GUI
	
	void OnGUI(){
		angleGUI();
		speedGUI();

		// Indicate turn
		GUI.Box(new Rect(Screen.width/1.5f, 3, 162, 23), isPlayer ? "Player's Turn" : "Opponent's Turn");
	}

	void angleGUI(){
		// current angle
		GUI.Box(new Rect(3, 3, 162, 23), "Current angle: " + transform.eulerAngles.z.ToString());
		
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

	void speedGUI(){
		// current speed
		GUI.Box(new Rect(200, 3, 162, 23), "Current speed: " + normalizedSpeed.ToString());
		
		// new speed
		speedStr = GUI.TextField(new Rect(200, 30, 33, 20), speedStr, 4);
		
		if (GUI.Button (new Rect (236, 30, 126, 20), "Click to set speed")) {
			if(float.TryParse(speedStr, out normalizedSpeed)){
				// success
				if(isPlayer){
					bulletSpeed = normalizedSpeed;
				}
				else {
					bulletSpeed = -normalizedSpeed;
				}
				
				print ("b: " + bulletSpeed);
			}
			else {
				// GUI.Label("not a #");
			}
		}
	}

	#endregion GUI


	#region Public Actions

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
		
		projectile.velocity = new Vector3(bulletSpeed, y, 0);

		BattleController.that.endTurn();
	}

	#endregion Public Actions


	#region Utils

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
		return bulletSpeed * Mathf.Tan(transform.eulerAngles.z * Mathf.Deg2Rad);
	}

	#endregion Utils
	
}

using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public bool isPlayer;

	Rigidbody bulletPrefab;

	public GunController controller;
	
	Vector3 initPos;
	float halfW;
	
	const int DT_THETA = 1;
	int minDeg, maxDeg;
	float dtMove = 0;

	string angleStr, speedStr;
	float angle, bulletSpeed;
    public float normalizedSpeed = 240;

	void Awake(){
		bulletPrefab = Resources.Load<Rigidbody>("Bullet");

		if(isPlayer){
			controller = gameObject.AddComponent<PlayerController>();
		}
		else {
			controller = gameObject.AddComponent<OpponentController>();
            controller.enabled = false;
		}

		initPos = transform.position;
		halfW = renderer.bounds.size.x/2;

		angleStr = "45";
		angle = transform.eulerAngles.z;
		
		speedStr = normalizedSpeed.ToString();
		bulletSpeed = -normalizedSpeed;

		if(isPlayer){
			halfW *= -1;
			bulletSpeed *= -1;
			
			minDeg = 270;
			maxDeg = 360;
		}
		else {
			minDeg = 0;
			maxDeg = 90;
		}

        setRandomAngle();
		setRandomPosition();
	}

	void Update(){
        if(controller.enabled){
            controller.CheckInput();
        }
	}

	#region GUI
	
	void OnGUI(){
        if(Battle.turn == Turn.PLAYER && isPlayer || Battle.turn == Turn.OPPONENT && !isPlayer){
            angleGUI();
            speedGUI();

            // Indicate turn
            GUI.Box(new Rect(Screen.width/1.5f, 3, 162, 23), isPlayer ? "Player's Turn" : "Opponent's Turn");
        }
	}

	void angleGUI(){

		
		// new angle
		angleStr = GUI.TextField(new Rect(3, 30, 30, 20), angleStr, 3);
		
		if (GUI.Button (new Rect (35, 30, 130, 20), "Click to set angle")) {
			if(float.TryParse(angleStr, out angle)){
				float dtAngle = 0;

                if(isPlayer) {
                    float tmp = 360 - angle;

                    //print("old: " + transform.eulerAngles.z);
                   // print("new: " + tmp);

                    if(tmp < transform.eulerAngles.z){
                        dtAngle = tmp - transform.eulerAngles.z;
                    }
                    else if(tmp > transform.eulerAngles.z){
                        dtAngle = tmp - transform.eulerAngles.z;
                    }
                } 
                else {
                    dtAngle = angle;
                }

                //print("dt: " + dtAngle);
                rotateTo(dtAngle);
			}
			else {
				// GUI.Label("not a #");
			}
		}


        // current angle
        float fixedAngle;
        if(isPlayer) {
            fixedAngle = (360 - transform.eulerAngles.z);
        } else {
            fixedAngle = transform.eulerAngles.z;
        }
        fixedAngle = Mathf.Floor(fixedAngle);

        GUI.Box(new Rect(3, 3, 162, 23), "Current angle: " + fixedAngle.ToString());
	}

	void speedGUI(){
		// current speed
		GUI.Box(new Rect(200, 3, 162, 23), "Current speed: " + normalizedSpeed.ToString());
		
		// new speed
		normalizedSpeed = GUI.HorizontalSlider(new Rect(226, 30, 126, 20), normalizedSpeed, 220, 280);
		
		if(isPlayer){
			bulletSpeed = normalizedSpeed;
		}
		else {
			bulletSpeed = -normalizedSpeed;
		}

//		speedStr = GUI.TextField(new Rect(200, 30, 33, 20), speedStr, 4);
//		
//		if(GUI.Button (new Rect (236, 30, 126, 20), "Click to set speed")) {
//			if(float.TryParse(speedStr, out normalizedSpeed)){
//				// success
//				if(isPlayer){
//					bulletSpeed = normalizedSpeed;
//				}
//				else {
//					bulletSpeed = -normalizedSpeed;
//				}
//			}
//			//else {
//				// GUI.Label("not a #");
//			//}
//		}
	}

	#endregion GUI


	#region Public Actions

	public void move(Dir dir){
        // TODO: if < 1 degree snap to target

		dtMove = (dir == Dir.UP) ? DT_THETA : -DT_THETA;
		rotateTo(dtMove);
	}

	public void shoot(){
        float bulletSizeX = isPlayer ? -2.5f : 2.5f;

        Vector3 pos = new Vector3(transform.position.x - halfW + bulletSizeX, transform.position.y + renderer.bounds.size.y/2, transform.position.z);
		Rigidbody projectile = Instantiate(bulletPrefab, pos, transform.rotation) as Rigidbody;

		if(isPlayer){
			CameraZoom.that.attachCameraToBullet(projectile);
		}

		Bullet b = projectile.GetComponent<Bullet>();
        b.init(isPlayer);

        float x = getBulletVelocityX();
		float y = getBulletVelocityY();

		projectile.velocity = new Vector3(x, y, 0);
        controller.enabled = false;
	}

	#endregion Public Actions


	#region Utils

    void setRandomAngle() {
        // starts at 270 or 30 by default
        rotateTo(Random.Range(-30, 60));
    }

	void setRandomPosition() {
		if(!isPlayer){
			GameObject.Find("Opponent").transform.Translate(Random.Range(-100, 100), 0, 0);
		}
	}

	void rotateTo(float dtAngle){
		if(transform.eulerAngles.z + dtAngle > minDeg && transform.eulerAngles.z + dtAngle < maxDeg){
			transform.RotateAround(
				new Vector3(initPos.x + halfW, initPos.y - renderer.bounds.size.y/2),
				Vector3.forward,
				dtAngle
			);
		}
		//else {
			// GUI.Label("outside of range");
		//}
	}

    float getBulletVelocityX(){
        // angle between the vector and the x-axis
        float angle = 0;

        if(isPlayer){
            angle = transform.eulerAngles.z - 270;
        } 
        else {
            angle = 90 - transform.eulerAngles.z;
        }

        return bulletSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);;
    }

    float getBulletVelocityY(){
        // angle between the vector and the y-axis
        float angle = 0;

        if(isPlayer) {
            angle = 360 - transform.eulerAngles.z;
        } 
        else {
            angle = transform.eulerAngles.z;
        }

        float vY = bulletSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);

        return isPlayer ? vY : -vY;
	}

	#endregion Utils
	
}

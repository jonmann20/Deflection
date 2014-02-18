using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public bool isPlayer;

	Rigidbody bulletPrefab;

	public GunController controller;
	
	Vector3 initPos;
	float halfW;
	
	const float DT_THETA = 0.3f;
	float minDeg, maxDeg;
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

		angle = transform.eulerAngles.z;
		
		speedStr = normalizedSpeed.ToString();
		bulletSpeed = -normalizedSpeed;

		if(isPlayer){
			halfW *= -1;
			bulletSpeed *= -1;
			
			minDeg = 270.5f;
			maxDeg = 359.5f;
		}
		else {
			minDeg = 0.5f;
			maxDeg = 89.5f;
		}

        setRandomAngle();
		//setRandomPosition();

        angleStr = normalizeAngle(transform.eulerAngles.z).ToString("f1");
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
        if(isPlayer) {
            angleStr = GUI.TextField(new Rect(3, 30, 40, 20), angleStr, 4);

            if(GUI.Button(new Rect(45, 30, 120, 20), "Click to set angle")) {
                if(float.TryParse(angleStr, out angle)) {
                    float dtAngle = 0;

                    float tmp = 270 + angle;

                    //print("old: " + transform.eulerAngles.z);
                    //print("new: " + tmp);

                    if(tmp < transform.eulerAngles.z) {
                        dtAngle = tmp - transform.eulerAngles.z;
                    }
                    else if(tmp > transform.eulerAngles.z) {
                        dtAngle = tmp - transform.eulerAngles.z;
                    }


                    //print("dt: " + dtAngle);
                    rotateBy(dtAngle);
                }
                else {
                    // GUI.Label("not a #");
                }
            }
        }

        // current angle
        float fixedAngle = normalizeAngle(transform.eulerAngles.z);
        GUI.Box(new Rect(3, 3, 162, 23), "Current angle: " + fixedAngle.ToString("f1"));
	}

	void speedGUI(){
		// current speed
		GUI.Box(new Rect(200, 3, 162, 23), "Current speed: " + normalizedSpeed.ToString("f1"));
		
		// new speed
		if(isPlayer){
            normalizedSpeed = GUI.HorizontalSlider(new Rect(226, 30, 126, 20), normalizedSpeed, 230, 250);
			bulletSpeed = normalizedSpeed;
		}
		else {
			bulletSpeed = -normalizedSpeed;
		}
	}

	#endregion GUI


	#region Public Actions

	public void move(Dir dir){
        // TODO: if < 1 degree snap to target

		dtMove = (dir == Dir.UP) ? DT_THETA : -DT_THETA;
		rotateBy(dtMove);
	}

	public void shoot(){
        controller.enabled = false;

        //float bulletSizeX = isPlayer ? -2.5f : 2.5f;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + renderer.bounds.size.y/2, transform.position.z);

        Rigidbody projectile = Instantiate(bulletPrefab, pos, transform.rotation) as Rigidbody;

        Bullet b = projectile.GetComponent<Bullet>();
        b.init(isPlayer);

        if(isPlayer){
            //CameraZoom.that.bulletTrans = projectile.transform;
        }

        float x = getBulletVelocityX();
        float y = getBulletVelocityY();

        projectile.velocity = new Vector3(x, y, 0);
    }

	#endregion Public Actions


	#region Utils

    void setRandomAngle() {
        // starts at 270 or 30 by default
        rotateBy(Random.Range(-30, 60));
    }

	void setRandomPosition() {
		if(!isPlayer){
			GameObject.Find("Opponent").transform.Translate(Random.Range(-100, 100), 0, 0);
		}
	}

    float normalizeAngle(float f) {
        if(isPlayer) {
            return (f - 270);
        }
        else {
            return -f + 90;
        }
    }

	public void rotateBy(float dtAngle){
		if(transform.eulerAngles.z + dtAngle > minDeg && transform.eulerAngles.z + dtAngle < maxDeg){
			transform.RotateAround(
				new Vector3(initPos.x + halfW, initPos.y - renderer.bounds.size.y/2),
				Vector3.forward,
				dtAngle
			);
		}
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

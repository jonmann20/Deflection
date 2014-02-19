using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public bool isPlayer;

	Rigidbody bulletPrefab;

	public GunController controller;
	
	Vector3 initPos;
	float halfW;
	
	float minDeg, maxDeg;

	string angleStr, speedStr;
    public float bulletSpeed = 0;
    float angle;
    const float initSpeed = 240;
    float normalizedSpeed = initSpeed;

	void Awake(){
		bulletPrefab = Resources.Load<Rigidbody>("Bullet");

		if(isPlayer){
			controller = gameObject.AddComponent<TurretController>();
		}
		else {
            //controller = gameObject.AddComponent<TurretController>();
            controller = gameObject.AddComponent<PlayerController>();
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
	}

	void Update(){
        if(controller.enabled){
            controller.CheckInput();
        }
	}


	#region Public Actions

	public void shoot(){
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Rigidbody projectile = Instantiate(bulletPrefab, pos, transform.rotation) as Rigidbody;

        Bullet b = projectile.GetComponent<Bullet>();
        b.init(isPlayer);

        float x = getBulletVelocityX();
        float y = getBulletVelocityY();

        projectile.velocity = new Vector3(x, y, 0);
    }

	#endregion Public Actions


	#region Utils

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

        return bulletSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);
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

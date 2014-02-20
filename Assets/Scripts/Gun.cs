using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public bool isBlue;

	Rigidbody bulletPrefab;

	public GunController controller;
	
	Vector3 initPos;
	float halfW;
	
	float minDeg, maxDeg;
    float bulletSpeed = 250;

	void Awake(){
		

        if(isBlue) {
            bulletPrefab = Resources.Load<Rigidbody>("BulletBlue");
            controller = gameObject.AddComponent<TurretController>();
        }
        else {
            bulletPrefab = Resources.Load<Rigidbody>("BulletRed");
            controller = gameObject.AddComponent<TurretController>();
        }

		initPos = transform.position;
		halfW = renderer.bounds.size.x/2;

        if(isBlue) {
			halfW *= -1;
			
			minDeg = 270.5f;
			maxDeg = 359.5f;
		}
		else {
            bulletSpeed *= -1;

			minDeg = 0.5f;
			maxDeg = 89.5f;
		}
	}

	void Update(){
        controller.CheckInput();
	}


	#region Public Actions

	public void shoot(){
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Rigidbody projectile = Instantiate(bulletPrefab, pos, transform.rotation) as Rigidbody;

        float x = getBulletVelocityX();
        float y = getBulletVelocityY();

        projectile.velocity = new Vector3(x, y, 0);
    }

    public void rotateBy(float dtAngle) {
        if(transform.eulerAngles.z + dtAngle > minDeg && transform.eulerAngles.z + dtAngle < maxDeg) {
            transform.RotateAround(
                new Vector3(initPos.x + halfW, initPos.y - renderer.bounds.size.y/2),
                Vector3.forward,
                dtAngle
            );
        }
    }

	#endregion Public Actions


	#region Utils

    float getBulletVelocityX(){
        // angle between the vector and the x-axis
        float angle = 0;

        if(isBlue) {
            angle = transform.eulerAngles.z - 270;
        } 
        else {
            angle = 90 - transform.eulerAngles.z;
        }

        float vx = bulletSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);

        // make more horizontal velocity
        if(isBlue) {
            vx += 24;
        }
        else {
            vx -= 24;
        }

        return vx;
    }

    float getBulletVelocityY(){
        // angle between the vector and the y-axis
        float angle = 0;

        if(isBlue) {
            angle = 360 - transform.eulerAngles.z;
        } 
        else {
            angle = transform.eulerAngles.z;
        }

        float vY = bulletSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);

        return isBlue ? vY : -vY;
	}

	#endregion Utils
	
}

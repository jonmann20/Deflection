using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public bool zoomOn = true;

	Camera cam;

	public Rigidbody bullet;

	public static CameraZoom that;

	void Awake(){
		that = this;
	}

	void Start(){
		cam = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update(){
		cam.fieldOfView = 48;
		//transform.Translate(-200, 0, 0);
		print (bullet);

		if(bullet != null){
			cam.fieldOfView = 40;
			transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y, -10);
		}
		else {
			cam.fieldOfView = 60;
			transform.position = new Vector3(0, 100, -10);
		}
	}

	public void attachCameraToBullet(Rigidbody b){
		bullet = b;
	}
}

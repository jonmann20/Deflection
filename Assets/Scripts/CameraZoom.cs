using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public static CameraZoom that;
	bool switchingToZoom = false;
	//public bool zoomOn = true;

	Camera cam;

	public Rigidbody bullet;


	void Awake(){
		that = this;
	}

	void Start(){
		cam = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update(){
		//print (bullet);

		if(bullet != null && !switchingToZoom){
			//toggleZoom(true);
			cam.fieldOfView = 40;
			transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y, -10);
		}
		else {
			cam.fieldOfView = 60;
			transform.position = new Vector3(0, 100, -10);
		}
	}


	void toggleZoom(bool turnZoomOn){
		if(turnZoomOn){
			print ("moving");
			switchingToZoom = true;
			Utils.that.MoveToPosition(transform, GameObject.Find("Player").transform.position, 0.5f, null);
		}
	}

	void attachZoomOn(){
		switchingToZoom = false;

		cam.fieldOfView = 40;
		transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y, -10);
	}

	public void attachCameraToBullet(Rigidbody b){
		bullet = b;
	}
}

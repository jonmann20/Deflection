using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public static CameraZoom that;
	bool switchingToZoom = false;
	public bool zoomIsOn = false;

	public Vector3 bulletPos;
    public Transform bulletTrans;


    float centerX = 0, size = -10;

	void Awake(){
		that = this;
	}

    void Update(){
        if(bulletTrans != null) {
            transform.position = new Vector3(bulletTrans.position.x, bulletTrans.position.y > 7.5f ? bulletTrans.position.y : 7.5f, -10);
        }
    }

	public void toggleZoom(bool turnZoomOn, Vector3 pos){
        bulletTrans = null;
        bulletPos = pos;

		if(turnZoomOn){    
            //StartCoroutine(Utils.that.MoveToPosition(transform, new Vector3(pos.x, pos.y, -10), 0.7f, null));
            //StartCoroutine(Utils.that.MoveToPosition(camera, 56, 0.6f, finishZoomIn));
            finishZoomIn();
		}
        else {
            //calcBoundingBox();

            //StartCoroutine(Utils.that.MoveToPosition(transform, new Vector3(centerX, 100, -10), 0.7f, null));
            //StartCoroutine(Utils.that.MoveToPosition(camera, 60, 0.6f, finishZoomOut));

            finishZoomOut();
        }
	}

    void finishZoomIn(){
        zoomIsOn = true;

        //Gun g = Battle.that.player.GetComponent<Gun>();
        //bulletTrans = g.doShot(bulletPos);
    }

    void finishZoomOut(){
        zoomIsOn = false;

        Battle.that.opponent.GetComponent<Gun>().controller.enabled = true;
    }


    void calcBoundingBox() {
        Vector3 pl = Battle.that.player.transform.position;
        Vector3 op = Battle.that.opponent.transform.position;

        centerX = (op.x + pl.x) / 2;

        // calculate required zoom to see both houses
        //size = Mathf.Max(Mathf.Abs(pl.y - op.y), Mathf.Abs(op.x - pl.x))/2;

        // some padding
       // size *= 1.1f;
    }

}

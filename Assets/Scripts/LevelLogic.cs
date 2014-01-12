using UnityEngine;
using System.Collections;

public class LevelLogic : MonoBehaviour {

	public static LevelLogic that;

	public GameObject sun;
	public GameObject sunLBound;
	public GameObject sunRBound;

	bool doLerp = false;
	float i = 0f;
	float rate = 0f;

	void Awake(){
		that = this;
	}

	void Update () {
		if (ProjectileFired.housesDestroyed >= 3) {
			Application.LoadLevel("end");
		}

		if (doLerp) {
			MoveObject (that.sun.transform, that.sunLBound.transform.position, that.sunRBound.transform.position, 4f);
		}
	}

	public static void endTurn(){
		that.doLerp = true;
	}

	void MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
		rate = 1f / time;

		if (i < 1f){
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
		}
	}
}

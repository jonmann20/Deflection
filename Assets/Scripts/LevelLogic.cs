using UnityEngine;
using System.Collections;

public class LevelLogic : MonoBehaviour {

	void Update () {
		if (ProjectileFired.housesDestroyed >= 1) {
			Application.LoadLevel("end");
		}
	}
}

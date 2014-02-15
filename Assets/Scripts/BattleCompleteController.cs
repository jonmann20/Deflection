using UnityEngine;
using System.Collections;

public class BattleCompleteController : MonoBehaviour {

	public GameObject status;
	public GameObject totalTime;
	
	
	void Start () {
		status.GetComponent<TextMesh>().text = "You " + BattleController.winLossStatus;
		totalTime.GetComponent<TextMesh>().text = Clock.strTime;
	}

	void Update () {
	
	}
}

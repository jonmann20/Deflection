using UnityEngine;
using System.Collections;

// TODO: move to global file
public enum Turn {PLAYER, OPPONENT}; 
public enum Dir {UP, DOWN};

public class BattleController : MonoBehaviour {

	public GameObject sun;
	//public GameObject sunLBound;
	//public GameObject sunRBound;

	public static BattleController that;
	public static Turn turn = Turn.PLAYER;
	public static int playerCubesDestroyed = 0;
	public static int opponentCubesDestroyed = 0;
	public static string winLossStatus = "";
	
	bool doLerp = false;
	float i = 0f;
	float rate = 0f;
	
	
	void Awake(){
		that = this;
	}
	
	void Update () {
		// battle over
		if(opponentCubesDestroyed >= 3) {
			winLossStatus = "win";
			Application.LoadLevel("end");
		}
		else if(playerCubesDestroyed >= 3){
			winLossStatus = "lose";
			Application.LoadLevel("end");
		}


		// turn indicator (sun)
		if (doLerp) {
			// TODO: freeze until callback from MoveObject??

			if(turn == Turn.PLAYER){
				turn = Turn.OPPONENT;
				//MoveObject(that.sun.transform, that.sunLBound.transform.position, that.sunRBound.transform.position, 2f);
			}
			else {
				turn = Turn.PLAYER;
				//MoveObject(that.sun.transform, that.sunRBound.transform.position, that.sunLBound.transform.position, 2f);
			}

			doLerp = false;
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

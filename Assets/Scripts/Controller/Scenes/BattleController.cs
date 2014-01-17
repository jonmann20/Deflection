using UnityEngine;
using System.Collections;

// TODO: move to global file
public enum Turn {PLAYER, OPPONENT}; 
public enum Dir {EMPTY, UP, DOWN, LEFT, RIGHT, TOP, BOTTOM};

public class BattleController : MonoBehaviour {

	public static Turn turn = Turn.PLAYER;
	public static int playerCubesDestroyed = 0;
	public static int opponentCubesDestroyed = 0;
	public static string winLossStatus = "";
	
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
	}
	
	public static void endTurn(){
		GunController.allowFire = true;

		if(turn == Turn.PLAYER){
			turn = Turn.OPPONENT;
		}
		else {
			turn = Turn.PLAYER;
		}
	}
	
//	void MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
//		rate = 1f / time;
//		
//		if (i < 1f){
//			i += Time.deltaTime * rate;
//			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
//		}
	//}
}

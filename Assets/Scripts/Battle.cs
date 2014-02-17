using UnityEngine;
using System.Collections;

public class Battle : MonoBehaviour {

    public static Battle that;

    public static Turn turn = Turn.PLAYER;
    public static int playerCubesDestroyed = 0;
    public static int opponentCubesDestroyed = 0;
    public static string winLossStatus = "";

    public GameObject player, opponent;

    void Awake(){
        that = this;
    }

    void Update(){
        // battle over
        if(opponentCubesDestroyed >= 3){
            turn = Turn.OVER;
            winLossStatus = "win";
            Application.LoadLevel("end");
        } 
        else if(playerCubesDestroyed >= 3){
            turn = Turn.OVER;
            winLossStatus = "lose";
            Application.LoadLevel("end");
        }
    }

    public void endTurn(){
        if(turn == Turn.PLAYER){
            turn = Turn.OPPONENT;

            opponent.GetComponent<Gun>().controller.enabled = true;
			CameraZoom.that.bullet = null;
        } 
        else if(turn == Turn.OPPONENT){
            turn = Turn.PLAYER;

            player.GetComponent<Gun>().controller.enabled = true;
			CameraZoom.that.bullet = null;
        }
    }

    //IEnumerator startTurn(Gun g) {
    //    yield return new WaitForSeconds(3f);
    //    g.enabled = true;
    //}
}

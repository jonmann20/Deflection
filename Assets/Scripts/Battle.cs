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
            winLossStatus = "win";
            Application.LoadLevel("end");
        } 
        else if(playerCubesDestroyed >= 3){
            winLossStatus = "lose";
            Application.LoadLevel("end");
        }
    }

    public void endTurn(){
        if(turn == Turn.PLAYER){
            turn = Turn.OPPONENT;

            player.GetComponent<Gun>().enabled = false;
            opponent.GetComponent<Gun>().enabled = true;
        } 
        else {
            turn = Turn.PLAYER;

            player.GetComponent<Gun>().enabled = true;
            opponent.GetComponent<Gun>().enabled = false;
        }
    }
}

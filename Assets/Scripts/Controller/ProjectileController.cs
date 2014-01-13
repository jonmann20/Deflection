using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if ((BattleController.turn == Turn.PLAYER) &&
		    (col.gameObject.tag == "opponentHouse")
	    ){
			Destroy (col.gameObject);
			++BattleController.opponentHousesDestroyed;
		}
		else if((BattleController.turn == Turn.OPPONENT) && 
		        (col.gameObject.tag == "playerHouse")
        ){
			Destroy (col.gameObject);
			++BattleController.playerHousesDestroyed;
		}
	}
	
	void OnBecameInvisible(){
		Destroy (gameObject);
		BattleController.endTurn ();
	}
}

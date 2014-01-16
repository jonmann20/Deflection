using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		print(BattleController.turn);

		if ((BattleController.turn == Turn.PLAYER) &&
		    (col.gameObject.tag == "opponentCube")
	    ){
			Destroy(col.gameObject);
			++BattleController.opponentCubesDestroyed;
		}
		else if((BattleController.turn == Turn.OPPONENT) && 
		        (col.gameObject.tag == "playerCube")
        ){
			Destroy(col.gameObject);
			++BattleController.playerCubesDestroyed;
		}
	}
	
	void OnBecameInvisible(){
		Destroy(gameObject);
		BattleController.endTurn ();
	}
}

using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	void checkCollision(Collision col, string cubeTag, ref int cubesDestroyed){
		if(col.gameObject.tag == cubeTag){
			Vector3 hit = col.contacts[0].normal;

			if(hit.y > 0.2){ // top
				Destroy(gameObject);
				Destroy(col.gameObject);
				++cubesDestroyed;
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if(BattleController.turn == Turn.PLAYER){
			checkCollision(col, "opponentCube", ref BattleController.opponentCubesDestroyed);
		}
		else if(BattleController.turn == Turn.OPPONENT){
			checkCollision(col, "playerCube", ref BattleController.playerCubesDestroyed);
		}

		Destroy(gameObject, 1.9f);
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}

	void OnDestroy(){
		BattleController.endTurn();
	}
}

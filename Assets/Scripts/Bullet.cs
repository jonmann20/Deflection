using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	bool isPlayer;
	public void init(bool b){
		isPlayer = b;
	}

	void checkCollision(Collision col, string cubeTag, ref int cubesDestroyed){
		if(col.gameObject.tag == cubeTag){
			Vector3 hit = col.contacts[0].normal;
			
			if(hit.y > 0.2f){ // top
				Destroy(gameObject);
				Destroy(col.gameObject);
				++cubesDestroyed;
			}
		}
	}
	
	void OnCollisionEnter(Collision col){
		if(isPlayer){
			checkCollision(col, "opponentCube", ref Battle.opponentCubesDestroyed);
		}
		else {
			checkCollision(col, "playerCube", ref Battle.playerCubesDestroyed);
		}
		
		Destroy(gameObject, 1.9f);
	}
	
	void OnBecameInvisible(){
		Destroy(gameObject);
	}
	
	void OnDestroy(){
		//Battle.that.endTurn();
	}
}

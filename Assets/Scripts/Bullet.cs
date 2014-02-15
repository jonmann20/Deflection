using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	bool isPlayer;

	public void init(bool b){
		isPlayer = b;

        // TODO: Destroy if no collision
	}

	void checkCollision(Collision col, ref int cubesDestroyed, string g){
        int numCubes = 2 - cubesDestroyed;

        //print("col: " + col.gameObject.GetInstanceID());
        //print(g + "H" + numCubes + ": " + GameObject.Find(g + "H" + numCubes).GetInstanceID());

        if(col.gameObject.GetInstanceID() == GameObject.Find(g + "H" + numCubes).GetInstanceID()) {
            gameObject.GetComponent<SphereCollider>().enabled = false;

            if(--numCubes != -1) {
                GameObject.Find(g + "H" + cubesDestroyed).GetComponent<BoxCollider>().enabled = true;
            }

            ++cubesDestroyed;
            Destroy(col.gameObject);
        }

        Destroy(gameObject, 1.5f);
	}
	
	void OnCollisionEnter(Collision col){
        if(isPlayer && col.gameObject.tag == "opponentCube") {
            checkCollision(col, ref Battle.opponentCubesDestroyed, "opp");
        }
        else if(!isPlayer && col.gameObject.tag == "playerCube") {
            checkCollision(col, ref Battle.playerCubesDestroyed, "p");
        }
        else {
            Destroy(gameObject, 2f);
        }
	}
	
	void OnDestroy(){
		Battle.that.endTurn();
	}
}

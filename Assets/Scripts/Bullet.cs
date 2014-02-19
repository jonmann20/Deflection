using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	bool isPlayer;

	public void init(bool b){
		isPlayer = b;
	}

    void Update(){
        if(transform.position.y < -90) {
            Destroy(gameObject);    
        }

    }
	
	void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Deflector") {
            // bounce off
            Vector3 forceVec = -rigidbody.velocity.normalized * 10000;
            rigidbody.AddForce(forceVec, ForceMode.Acceleration);
            print("ok");
        }
        else {
            Destroy(gameObject);
        }
	}
	
	void OnDestroy(){
		//Battle.that.endTurn();
	}
}

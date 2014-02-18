using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	bool isPlayer;

	public void init(bool b){
		isPlayer = b;
	}

    void Update(){
        //Vector3 a = transform.position;
        //Vector3 b = transform.TransformDirection(Vector3.back) * 400;

        //Debug.DrawRay(a, b);

        //if(Physics.Raycast(a, b)){
        //    print("hit");
        //}

        if(transform.position.y < -1000) {
            Destroy(gameObject);    // far below terrain
        }

    }

	void checkCollision(Collision col, ref int cubesLeft, string g){
        int topCubeIdx = cubesLeft-1;

        //print("col: " + col.gameObject.GetInstanceID());
        //print(g + "H" + numCubes + ": " + GameObject.Find(g + "H" + numCubes).GetInstanceID());

        if(col.gameObject.GetInstanceID() == GameObject.Find(g + "H" + topCubeIdx).GetInstanceID()) {      // top cube
            if(topCubeIdx != 0) {
                GameObject.Find(g + "H" + (topCubeIdx-1)).GetComponent<BoxCollider>().enabled = true;
            }

            --cubesLeft;

            killBullet();
            killHouse(col.gameObject, (g == "playerCube"));
        }
        else {
            Destroy(gameObject, 1.8f);      // hit target, but didn't destroy
        }
	}
	
	void OnCollisionEnter(Collision col){
        if(isPlayer && col.gameObject.tag == "opponentCube") {
            checkCollision(col, ref Battle.oCubesLeft, "opp");
        }
        else if(!isPlayer && col.gameObject.tag == "playerCube") {
            checkCollision(col, ref Battle.pCubesLeft, "p");
        }
        else {
            Destroy(gameObject, 1.8f);  // missed target
        }
	}
	
	void OnDestroy(){
		Battle.that.endTurn();
	}

    void killBullet() {
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;

        Destroy(gameObject, 1.3f);      // hit and destroyed target
    }

    void killHouse(GameObject house, bool isPlayerCube) {
        //if(isPlayerCube){
        //    Battle.that.pHouses.RemoveAt(Battle.that.pHouses.Count - 1);
        //}
        //else {
        //    Battle.that.oHouses.RemoveAt(Battle.that.oHouses.Count - 1);
        //    print(Battle.that.oHouses.Count);
        //}

        Destroy(house);
    }
}

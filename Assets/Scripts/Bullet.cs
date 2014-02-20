using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
    public bool isBlue;

    void Update(){
        if(transform.position.y < -90) {
            Destroy(gameObject);    
        }

    }

    //void FixedUpdate(){
    //    Vector3 direction = transform.position - lastPos;
    //    Ray ray = new Ray(lastPos, direction);

    //    Debug.DrawRay(transform.position, direction);


    //    RaycastHit hit;
    //    if(Physics.Raycast(ray, out hit, direction.magnitude)){
    //        print("hit");
    //    }

    //    lastPos = transform.position;
    //}
	
	void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Deflector") {
            // bounce off
            //Vector3 forceVec = -rigidbody.velocity.normalized * 10000;
            //rigidbody.AddForce(forceVec, ForceMode.Acceleration);
            //rigidbody.AddForce(0, -40, 0);

            print("col");
            rigidbody.velocity = -rigidbody.velocity;
        }
        else {
            Destroy(gameObject, 0.15f);
        }
	}
}

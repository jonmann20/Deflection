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
            GameAudio.playThud(transform.position);
            
            //print("col");
            rigidbody.velocity = -(rigidbody.velocity * 1.32f);
        }
        else {
            Destroy(gameObject, 0.15f);
        }
	}
}

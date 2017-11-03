using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
    // TODO: z-index

    public bool isBlue;

    void Awake(){
        if(isBlue) {
            GetComponent<Renderer>().material.color = new Color(78/255.0f, 131/255.0f, 170/255.0f);
        }
        else {
            GetComponent<Renderer>().material.color = new Color(191/255.0f, 79/255.0f, 46/255.0f);
        }
    }


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
        if(col.gameObject.tag != "Deflector") {
            Destroy(gameObject, 0.15f);
        }
	}
}

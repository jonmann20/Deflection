using UnityEngine;
using System.Collections;

public class Deflector : MonoBehaviour {

    public bool isBlue = false;

    float edge = 288;
    float dtX = 60;
    float dtY = 30;

    Vector3 newDeflectorPos;

    void Awake() {
        newDeflectorPos = transform.position;
    }

	void Update () {
        //----- Position

        if(isBlue) {
            if(Input.GetKeyDown(KeyCode.A)) {
                newDeflectorPos = transform.position;
                newDeflectorPos.x -= dtX;
            }
            if(Input.GetKeyDown(KeyCode.D)) {
                newDeflectorPos = transform.position;
                newDeflectorPos.x += dtX;
            }
            if(Input.GetKeyDown(KeyCode.W)) {
                newDeflectorPos = transform.position;
                newDeflectorPos.y += dtY;
            }
            if(Input.GetKeyDown(KeyCode.S)) {
                newDeflectorPos = transform.position;
                newDeflectorPos.y -= dtY;
            }
        }
        else {
            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                newDeflectorPos = transform.position;
                newDeflectorPos.x -= dtX;
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                newDeflectorPos = transform.position;
                newDeflectorPos.x += dtX;
            }
            if(Input.GetKeyDown(KeyCode.UpArrow)) {
                newDeflectorPos = transform.position;
                newDeflectorPos.y += dtY;
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)) {
                newDeflectorPos = transform.position;
                newDeflectorPos.y -= dtY;
            }
        }

        checkScreenEdgeCollision();
        checkBallCollision();
	}

    void FixedUpdate() {
        // move
        rigidbody.MovePosition(newDeflectorPos);
    }

    void handleBallCollision(RaycastHit hit){
        if(hit.collider.gameObject.tag == "Bullet") {

            Bullet b = hit.collider.GetComponent<Bullet>();
            if(isBlue && !b.isBlue || !isBlue && b.isBlue) {     // blue deflector and red ball OR red deflector and blue ball
                GameAudio.playThud(transform.position);

                //print("hit");
                float pad = 2.1f;
                Vector3 offset = Vector3.zero;
                if(hit.collider.transform.position.x > newDeflectorPos.x) {
                    offset = Vector3.left * pad;
                }
                else if(hit.collider.transform.position.x < newDeflectorPos.x) {
                    offset = Vector3.right * pad;
                }
                else {
                    // landed on top... now what???
                }



                hit.collider.transform.position = newDeflectorPos + offset;
                //hit.collider.rigidbody.velocity = -hit.collider.rigidbody.velocity;
            }
        }
    }

    void checkScreenEdgeCollision() {
        if(transform.position.y > 271) {
            transform.position = new Vector3(transform.position.x, 271, transform.position.z);
        }
        else if(transform.position.y < 5) {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }

        if(transform.position.x > edge) {
            transform.position = new Vector3(edge, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < -edge) {
            transform.position = new Vector3(-edge, transform.position.y, transform.position.z);
        }
    }

    void checkBallCollision() {
        for(int i=-17; i < 17; ++i) {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + i, transform.position.z);    // TODO: raycast in vertical direction also
            Vector3 newPos = new Vector3(newDeflectorPos.x, newDeflectorPos.y + i, newDeflectorPos.z);
            Vector3 direction = newPos - pos;

            Ray ray = new Ray(pos, direction);

            Debug.DrawRay(pos, direction);

            RaycastHit rayCastHit;
            if(Physics.Raycast(ray, out rayCastHit, direction.magnitude)) {
                handleBallCollision(rayCastHit);
                break;
            }
        }
    }
}



#region Archive

//transform.position = newDeflectorPos;
//Vector3 newVel = (newDeflectorPos - transform.position)/Time.deltaTime;
//print(newVel);
//if(newVel.x > maxSpeed.x) {
//    newVel.x = maxSpeed.x;
//}

//if(newVel.y > maxSpeed.y) {
//    newVel.y = maxSpeed.y;
//}
//Vector3 direction = (newDeflectorPos - transform.position).normalized;
//rigidbody.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);


//rigidbody.velocity = newVel;


//Vector3 newDirForce = (newDeflectorPos - transform.position)/Time.deltaTime;
//rigidbody.AddForce(newDirForce);


//Vector3 mousePos = Input.mousePosition;

//mousePos.z = transform.position.z - cam.transform.position.z;
//newDeflectorPos = cam.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
//newDeflectorPos.z = 290;

//----- Rotation
//if(Input.GetKey(KeyCode.W)){
//    transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
//}

//if(Input.GetKey(KeyCode.S)){
//    transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
//}

//float rotateSpeed = 140;

#endregion Archive
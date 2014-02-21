using UnityEngine;
using System.Collections;

public class Deflector : MonoBehaviour {

    // TODO: z-index

    public bool isBlue = false;

    float dtX = 51;
    float dtY = 30;

    float extraVx = 1.18f;

    Vector3 newDeflectorPos;

    bool flippedVx = false;

    public static Deflector that;

    void Awake() {
        that = this;
        newDeflectorPos = transform.position;
    }

	void Update () {
        //----- Position
        if(isBlue) {
            if(Input.GetKeyDown(KeyCode.A)) {
                getNewX(-dtX);
            }

            if(Input.GetKeyDown(KeyCode.D)) {
                getNewX(dtX);
            }

            if(Input.GetKeyDown(KeyCode.W)) {
                getNewY(dtY);
            }

            if(Input.GetKeyDown(KeyCode.S)) {
                getNewY(-dtY);
            }
        }
        else {
            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                getNewX(-dtX);
            }

            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                getNewX(dtX);
            }

            if(Input.GetKeyDown(KeyCode.UpArrow)) {
                getNewY(dtY);
            }

            if(Input.GetKeyDown(KeyCode.DownArrow)) {
                getNewY(-dtY);
            }
        }

        checkBallCollision();
        flippedVx = false;
	}

    void FixedUpdate() {
        // move
        rigidbody.MovePosition(newDeflectorPos);
    }

    
    void OnCollisionEnter(Collision c) {
        if(c.gameObject.tag == "Bullet") {
            //print("defl");

            GameAudio.playThud(c.transform.position);
            
            // reverse x direction
            c.rigidbody.velocity = new Vector3(-(c.rigidbody.velocity.x * extraVx), c.rigidbody.velocity.y, 0);
            flippedVx = true;
        }
    }

    void handleBallCollision(RaycastHit hit){
        if(hit.collider.gameObject.tag == "Bullet") {

            Bullet b = hit.collider.GetComponent<Bullet>();
            if(isBlue && !b.isBlue || !isBlue && b.isBlue) {     // blue deflector and red ball OR red deflector and blue ball
                GameAudio.playThud(transform.position);

                //print("hit");
                float pad = 5.1f;
                Vector3 offset = Vector3.zero;
                if(hit.collider.transform.position.x > newDeflectorPos.x) {
                    offset = Vector3.left * pad;

                    // ball was headed away from deflector
                    if(!flippedVx) {
                        hit.collider.rigidbody.velocity = new Vector3((hit.collider.rigidbody.velocity.x * extraVx), hit.collider.rigidbody.velocity.y, 0);
                    }
                }
                else if(hit.collider.transform.position.x < newDeflectorPos.x) {
                    offset = Vector3.right * pad;
                    
                    // ball was headed toward deflector
                    if(!flippedVx) {
                        hit.collider.rigidbody.velocity = new Vector3(-(hit.collider.rigidbody.velocity.x * extraVx), hit.collider.rigidbody.velocity.y, 0);
                    }
                }
                else {
                    // landed on top... now what???
                }

                hit.collider.transform.position = newDeflectorPos + offset;
                
            }
        }
    }

    void getNewX(float dtX) {
        newDeflectorPos = transform.position;

        int maxCrossOver = 48;

        if(isBlue) {
            if(newDeflectorPos.x + dtX >= maxCrossOver) {
                newDeflectorPos.x = maxCrossOver;
            }
            else if(newDeflectorPos.x + dtX <= -268) {
                newDeflectorPos.x = -268;
            }
            else {  // in between
                newDeflectorPos.x += dtX;
            }
        }
        else {
            if(newDeflectorPos.x + dtX >= 268) {
                newDeflectorPos.x = 268;
            }
            else if(newDeflectorPos.x + dtX <= -maxCrossOver) {
                newDeflectorPos.x = -maxCrossOver;
            }
            else {  // in between
                newDeflectorPos.x += dtX;
            }
        }
    }

    void getNewY(float dtY){
        newDeflectorPos = transform.position;

        if(newDeflectorPos.y + dtY >= 271) {
            newDeflectorPos.y = 271;
        }
        else if(newDeflectorPos.y + dtY <= 5) {
            newDeflectorPos.y = 5;
        }
        else {   // in between
            newDeflectorPos.y += dtY;
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
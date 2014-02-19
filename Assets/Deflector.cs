using UnityEngine;
using System.Collections;

public class Deflector : MonoBehaviour {

    float rotateSpeed = 140;
	
	// Update is called once per frame
	void Update () {
        Vector3 m = Input.mousePosition;

        m.x -= Screen.width/2;
        m.y -= Screen.height/4;

        if(m.y > 271){
            m.y = 271;
        }
        else if(m.y < 5){
            m.y = 5;
        }

        if(m.x > 300){
            m.x = 300;
        }
        else if(m.x < -300){
            m.x = -300;
        }

        transform.position = new Vector3(m.x, m.y, 290);

        

        if(Input.GetKey(KeyCode.W)){
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.S)){
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }


	}
}

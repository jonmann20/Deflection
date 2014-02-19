using UnityEngine;
using System.Collections;

public class TurretController : GunController {

    bool isMoving = false;
    bool getNewAngle = true;

    float newAngle = 0;
    float angleOffset = 0;
    float dtAngle = 0;
    Dir dir = Dir.NONE;

    public override void CheckInput() {
        if(isMoving) {
            doMovement();
        }
        else if(getNewAngle) {
            calcAngle();
        }
    }


    void doMovement(){
        dtAngle = angleOffset * (Time.deltaTime/1.2f);
        dtAngle = (dir == Dir.UP) ? dtAngle : -dtAngle;

        gun.rotateBy(dtAngle);

        if(dir == Dir.UP && gun.transform.eulerAngles.z >= newAngle || dir == Dir.DOWN && gun.transform.eulerAngles.z <= newAngle) {
            isMoving = false;
            getNewAngle = true;    // reset for next shot

            gun.shoot();
        }
    }

    void calcAngle(){
        float angle = Random.Range(20, 70) + 270;

        angleOffset = Mathf.Abs(gun.transform.eulerAngles.z - angle);

        if(angle > gun.transform.eulerAngles.z){       // up
            dir = Dir.UP;
            newAngle = gun.transform.eulerAngles.z + angleOffset;
        }
        else if(angle < gun.transform.eulerAngles.z){  // down
            dir = Dir.DOWN;
            newAngle = gun.transform.eulerAngles.z - angleOffset;
        }


        isMoving = true;
        getNewAngle = false;
    }
}
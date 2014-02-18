using UnityEngine;
using System.Collections;

public class OpponentController : GunController {

    public Difficulty difficulty = Difficulty.HARD;

    // FOR DEBUGGING: ---user control---
    //public override void CheckInput() {
    //    if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    //        gun.move(Dir.DOWN);
    //    else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
    //        gun.move(Dir.UP);

    //    if(Input.GetKeyDown(KeyCode.Space))
    //        gun.shoot();
    //}

    public override void CheckInput() {
        float angle = findBestAngle();
        //print("best angle: " + angle);

        float diff = Mathf.Abs(transform.eulerAngles.z - angle);
        bool coarseTune = (diff <= 10);
        bool fineTune = (diff <= 5);

        float dtAngle = 0;

        switch(difficulty) {
            case Difficulty.EASY:
                dtAngle = fineTune ? 1 : (coarseTune ? 2 : 3);
                break;
            case Difficulty.MEDIUM:
                dtAngle = fineTune ? 1 : (coarseTune ? 2 : 6);
                break;
            case Difficulty.HARD:
                dtAngle = fineTune ? 1 : (coarseTune ? 2 : 9);
                break;

        }

        // TODO: move to other size of missing
		//TODO: have a error float, and random number generator within the error range

        doMovement(angle, dtAngle);
        gun.shoot();
    }

    void doMovement(float angle, float dtAngle){
        // TODO: slerp??

        if(angle > transform.eulerAngles.z) {       // up
            gun.rotateBy(dtAngle);
        } 
        else if(angle < transform.eulerAngles.z) {  // down
            gun.rotateBy(-dtAngle);
        }
    }

    float findBestAngle(){
        // theta = 1/2 arcsin(gd / v^2)

        float dist = Mathf.Abs(GameObject.Find("pGun").transform.position.x - gameObject.transform.position.x);
        float inAsin = (Physics.gravity.magnitude * dist) / (gun.normalizedSpeed*gun.normalizedSpeed);

        if(inAsin > 1) {
            --inAsin;
        }

        float angle = 0.5f * Mathf.Asin(inAsin);

        //print("best angle (rad): " + angle);
        return angle * Mathf.Rad2Deg;
    }
}

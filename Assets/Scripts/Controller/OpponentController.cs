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
        //print(angle);
        float diff = Mathf.Abs(transform.eulerAngles.z - angle);
        bool coarseTune = (diff <= 10);
        bool fineTune = (diff <= 5);

        //print(diff);
        int numMoves = 0;

        switch(difficulty) {
            case Difficulty.EASY:
                numMoves = fineTune ? 1 : (coarseTune ? 2 : 3);
                break;
            case Difficulty.MEDIUM:
                numMoves = fineTune ? 1 : (coarseTune ? 2 : 6);
                break;
            case Difficulty.HARD:
                numMoves = fineTune ? 1 : (coarseTune ? 2 : 9);
                break;

        }

        // TODO: move to other size of missing

        for(int i=0; i < numMoves; ++i) {
            doMovement(angle);
        }

        gun.shoot();
    }

    void doMovement(float angle){
        if(angle > transform.eulerAngles.z) {
            gun.move(Dir.UP);
        } 
        else if(angle < transform.eulerAngles.z) {
            gun.move(Dir.DOWN);
        }
    }

    float findBestAngle(){
        // theta = 1/2 arcsin(gd / v^2)

        // TODO: calculate actual distance dynamically
        float dist = 498.8f;

        float angle = 0.5f * Mathf.Asin((Physics.gravity.magnitude * dist ) / (gun.normalizedSpeed*gun.normalizedSpeed));
        return angle * Mathf.Rad2Deg;
    }
}

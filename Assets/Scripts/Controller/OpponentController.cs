using UnityEngine;
using System.Collections;

public class OpponentController : GunController {

    public Difficulty difficulty = Difficulty.MEDIUM;

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

        float diff = Mathf.Abs(transform.eulerAngles.z - angle);
        bool fineTune = (diff < 10);

        //print(diff);
        int numMoves = 0;

        switch(difficulty) {
            case Difficulty.EASY:
                numMoves = fineTune ? 1 : 3;
                break;
            case Difficulty.MEDIUM:
                numMoves = fineTune ? 3 : 6;
                break;
            case Difficulty.HARD:
                numMoves = fineTune ? 4 : 9;
                break;

        }

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
        float dist = 510 - (Battle.playerCubesDestroyed * 5);   // player: -255, opp: 255

        float angle = 0.5f * Mathf.Asin((Physics.gravity.magnitude * dist ) / (gun.normalizedSpeed*gun.normalizedSpeed));
        return angle * Mathf.Rad2Deg;
    }
}

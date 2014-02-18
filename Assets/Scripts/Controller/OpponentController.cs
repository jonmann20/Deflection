using UnityEngine;
using System.Collections;

public class OpponentController : GunController {

    public Difficulty difficulty = Difficulty.HARD;

    bool isMoving = false;
    bool firstRun = true;
    float newAngle = 0;
    float angleOffset = 0;
    float dtAngle = 0;
    Dir dir = Dir.NONE;

    public override void CheckInput() {
        if(isMoving) {
            doMovement();
        }
        else if(firstRun) {
            calcAngle();
        }
    }


    void doMovement(){
        dtAngle = angleOffset * (Time.deltaTime/0.9f);
        dtAngle = (dir == Dir.UP) ? dtAngle : -dtAngle;
        gun.rotateBy(dtAngle);

        if(dir == Dir.UP && gun.transform.eulerAngles.z >= newAngle || dir == Dir.DOWN && gun.transform.eulerAngles.z <= newAngle) {
            // TODO: rotateTo() to ensure correct angle reached
            //gun.transform.eulerAngles.z = newAngle;

            isMoving = false;
            firstRun = true;    // reset for next shot
            gun.shoot();
        }
    }

    void calcAngle(){
        float angle = findBestAngle();
        print("best angle: " + angle);

        // TODO: move to other side of missing
        // TODO: have a error float, and random number generator within the error range

        float diff = Mathf.Abs(gun.transform.eulerAngles.z - angle);
        bool coarseTune = (diff <= 10);
        bool fineTune = (diff <= 5);

        //angleOffset = 0;

        //switch(difficulty) {
        //    case Difficulty.EASY:
        //        angleOffset = fineTune ? 1 : (coarseTune ? 2 : 3);
        //        break;
        //    case Difficulty.MEDIUM:
        //        angleOffset = fineTune ? 1 : (coarseTune ? 2 : 6);
        //        break;
        //    case Difficulty.HARD:
        //        angleOffset = fineTune ? 1 : (coarseTune ? 2 : 9);
        //        break;
        //}

        angleOffset = diff;

        if(angle > gun.transform.eulerAngles.z){       // up
            dir = Dir.UP;
            newAngle = gun.transform.eulerAngles.z + angleOffset;
        }
        else if(angle < gun.transform.eulerAngles.z){  // down
            dir = Dir.DOWN;
            newAngle = gun.transform.eulerAngles.z - angleOffset;
        }

        firstRun = false;
        isMoving = true;
    }

    Vector2 calcDist(){
        Vector2 d = new Vector2();

        int count = Battle.pCubesLeft-1;
        print("c: " + count);

        if(count > -1){
            d.x = Mathf.Abs(Battle.that.pHouses[count].transform.position.x - gun.transform.position.x);   // distance between gun and top cube
            d.y = Mathf.Abs(Battle.that.pHouses[count].transform.position.y - gun.transform.position.y);   // height between gun and top cube
            //d.y = 0;
        }

        print(d);

        return d;
    }

    float findBestAngle(){
        // theta = arctan((v^2 +- sqrt(v^4 - g(gx^2 + 2yv^2)))/gx)   ..... gamedev: http://gamedev.stackexchange.com/questions/53552/how-can-i-find-a-projectiles-launch-angle

        //Vector2 dist = calcDist();
        //float x = dist.x;
        //float y = dist.y;

        //float v = gun.bulletSpeed;                      // speed of bullet
        //float v2 = v*v;
        //float g = Mathf.Abs(Physics.gravity.y);         // gravity constant
        //float angle = Mathf.Atan((v2 + Mathf.Sqrt(v2*v2 - g*(g*x*x + 2*y*v2))) / (g*x));    // always take positive root

        //// TODO: check for NaN, increase speed??

        //return angle * Mathf.Rad2Deg;

        float cheat = 0;

        switch(Battle.pCubesLeft) {
            case 3:
                cheat = 36;
                break;
            case 2:
                cheat = 34;
                break;
            case 1:
                cheat = 32;
                break;
        }
        return 90 - cheat;
    }
}


// theta = 1/2 arcsin(gd / v^2)     ..... wikipedia: http://en.wikipedia.org/wiki/Trajectory_of_a_projectile
//float inAsin = (Physics.gravity.magnitude * dist) / (gun.bulletSpeed*gun.bulletSpeed);

//if(inAsin > 1) {
//    print("weird");
//    --inAsin;
//}

//float angle = 0.5f * Mathf.Asin(inAsin);
using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public bool isBlue = false;

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Bullet") {
            if(isBlue) {
                --Battle.numPointsBlue;
            }
            else {
                --Battle.numPointsRed;
            }

            Destroy(col.gameObject);
            playGoalAnimation();
        }
    }


    void playGoalAnimation() {
        GameAudio.play("score");
    }
}

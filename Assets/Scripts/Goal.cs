using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public bool isBlue = false;

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Bullet") {
            if(isBlue) {
                --Battle.numPointsBlue;
                GameAudio.playScore(true);
            }
            else {
                --Battle.numPointsRed;
                GameAudio.playScore(false);
            }

            playGoalAnimation(col);
        }
    }


    void playGoalAnimation(Collider col) {
        Behaviour halo = col.GetComponent("Halo") as Behaviour;
        halo.enabled = true;

        Destroy(col.gameObject, 0.6f);        
    }
}

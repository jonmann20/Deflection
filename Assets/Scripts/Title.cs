using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	
	void Update(){
        if(Input.GetKey(KeyCode.Return)) {
            Application.LoadLevel("main");
        }
	}

    void OnGUI(){
        Utils.scaleGUI();

        Utils.placeTxt("Deflection", 88, Utils.HALFW, Utils.FULLH/5.3f);
        Utils.blinkTxt("Press Enter", 66, Utils.HALFW, Utils.FULLH/2.45f);

        int size = 46;

        Utils.placeTxt("Player 1 (blue): WASD movement", size, Utils.HALFW, Utils.FULLH/1.7f);
        Utils.placeTxt("Player 2 (red):  Arrow Keys movement", size, Utils.HALFW, Utils.FULLH/1.5f);
        Utils.placeTxt("Goal: keep the other player from scoring " + Battle.NUM_TO_WIN + " points", size, Utils.HALFW, Utils.FULLH/1.25f);
        Utils.placeTxt("(block the opposing color's balls from crossing the goal line)", size, Utils.HALFW, Utils.FULLH/1.15f);
    }
}


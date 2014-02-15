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

        Utils.placeTxt("tyBomber", 86, Utils.HALFW, Utils.FULLH/5);
        Utils.placeTxt("Press Enter", 86, Utils.HALFW, Utils.FULLH/1.8f);
    }
}


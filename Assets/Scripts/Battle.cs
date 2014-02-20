using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle : MonoBehaviour {

    public static Battle that;

    
    public static int NUM_TO_WIN = 15;
    public static int numPointsBlue = NUM_TO_WIN, numPointsRed = NUM_TO_WIN;

    public static bool didBlueWin = false;

    void Awake(){
        that = this;
    }

    void Start() {
        GameAudio.play("bgMusic");
    }

    void Update() {
        if(numPointsBlue <= 0) {
            finish(false);
        }

        if(numPointsRed <= 0) {
            finish(true);
        }
    }

    void finish(bool b) {
        didBlueWin = b;
        numPointsBlue = NUM_TO_WIN;
        numPointsRed = NUM_TO_WIN;
        Application.LoadLevel("end");
    }

    void OnGUI() {
        Utils.scaleGUI();

        Utils.placeTxt("Blue: " + numPointsBlue.ToString(), 40, 80, 55);
        Utils.placeTxt("Red: " + numPointsRed.ToString(), 40, Utils.FULLW - 80, 55);
    }
}

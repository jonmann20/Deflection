using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle : MonoBehaviour {

    public static Battle that;

    public static int numPointsBlue = 0, numPointsRed = 0;
    public static int NUM_TO_WIN = 15;

    public static bool didBlueWin = false;

    void Awake(){
        that = this;
    }

    void Update() {
        if(numPointsBlue >= NUM_TO_WIN) {
            finish(false);
        }

        if(numPointsRed >= NUM_TO_WIN) {
            finish(true);
        }
    }

    void finish(bool b) {
        didBlueWin = b;
        numPointsBlue = 0;
        numPointsRed = 0;
        Application.LoadLevel("end");
    }

    void OnGUI() {
        Utils.scaleGUI();

        Utils.placeTxt("Blue: " + numPointsBlue.ToString(), 40, 80, 55);
        Utils.placeTxt("Red: " + numPointsRed.ToString(), 40, Utils.FULLW - 80, 55);
    }
}

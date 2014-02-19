using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle : MonoBehaviour {

    public static Battle that;

    public static Turn turn = Turn.PLAYER;
    public static int pCubesLeft = 3;
    public static int oCubesLeft = 3;
    public static string winLossStatus = "";

    public GameObject player, opponent;
    public List<GameObject> pHouses, oHouses;

    public static int numPoints = 0;

    void Awake(){
        that = this;

        pHouses = new List<GameObject>();
        pHouses.Add(GameObject.Find("pH0"));
        pHouses.Add(GameObject.Find("pH1"));
        pHouses.Add(GameObject.Find("pH2"));

        oHouses = new List<GameObject>();
        oHouses.Add(GameObject.Find("oppH0"));
        oHouses.Add(GameObject.Find("oppH1"));
        oHouses.Add(GameObject.Find("oppH2"));
    }

    void OnGUI() {
        Utils.scaleGUI();

        Utils.placeTxt(numPoints.ToString(), 40, 25, 75);
    }
}

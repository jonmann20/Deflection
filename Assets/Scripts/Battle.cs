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

    void Start() {
        CameraZoom.that.toggleZoom(true, player.transform.position);
    }

    void Update(){
        // battle over
        if(oCubesLeft <= 0){
            turn = Turn.OVER;
            winLossStatus = "win";
            Application.LoadLevel("end");
        } 
        else if(pCubesLeft <= 0){
            turn = Turn.OVER;
            winLossStatus = "lose";
            Application.LoadLevel("end");
        }
    }

    public void endTurn(){
        if(turn == Turn.PLAYER){
            turn = Turn.OPPONENT;

            if(opponent != null) {
                //CameraZoom.that.toggleZoom(false, opponent.transform.position);
                opponent.GetComponent<Gun>().controller.enabled = true;
            }
        } 
        else if(turn == Turn.OPPONENT){
            turn = Turn.PLAYER;

            if(player != null) {
                CameraZoom.that.toggleZoom(true, player.transform.position);
                player.GetComponent<Gun>().controller.enabled = true;
            }
        }
    }

    //IEnumerator startTurn(Gun g) {
    //    yield return new WaitForSeconds(3f);
    //    g.enabled = true;
    //}
}

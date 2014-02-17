using UnityEngine;
using System.Collections;

public class BattleComplete : MonoBehaviour {

	void Update(){
		if(Input.GetKeyDown(KeyCode.Return)){
			Application.LoadLevel("main");
		}
	}

    void OnGUI() {
        Utils.scaleGUI();

        Utils.placeTxt("Battle Complete", 86, Utils.HALFW, Utils.FULLH/5);
        Utils.placeTxt("Status:", 69, Utils.FULLW/4.34f, Utils.FULLH/2.2f);
        Utils.placeTxt("Total Time:", 69, Utils.FULLW/5, Utils.FULLH/1.8f);

        Utils.placeTxt("You " + Battle.winLossStatus, 69, Utils.FULLW/2.5f, Utils.FULLH/2.2f);
        Utils.placeTxt(Clock.strTime, 69, Utils.FULLW/2.5f, Utils.FULLH/1.8f);
    }
}

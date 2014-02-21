using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

	float totalTime;
	int h, m, s;
	string hr, min, sec;
	
	public static string strTime = "00:00:00";

    int prevS = 0;

    bool showUpdate = false;
    int updateTimer = 0;

	void Start() {
		totalTime = 0.0f;
	}
	
	void Update() {
		totalTime += Time.deltaTime;
	}

    void OnGUI() {
        updateTimeDisplay();

        Utils.scaleGUI();
        Utils.placeTxt(strTime, 40, Utils.HALFW, 55);
        Utils.placeTxt("Min time between shots: " + TurretController.minTimeBtwShots.ToString("f2") + " sec", 30, 285, Utils.FULLH - 45, true);
        Utils.placeTxt("Deflector height: " + Deflector.that.transform.localScale.x.ToString("f2"), 30, 316, Utils.FULLH - 15, true);

        if(showUpdate && updateTimer != 0){
            Utils.placeTxt("Difficulty Increased!!", 40, Utils.HALFW, Utils.FULLH - 45);
            --updateTimer;
        }
    }
	
	void updateTimeDisplay(){
		h = (int)(totalTime / 3600);
		m = (int)(totalTime / 60);
		s = (int)(totalTime % 60);
		
		hr = ((h < 10) ? "0" : "") + h.ToString();
		min = ((m < 10) ? "0" : "") + m.ToString();
		sec = ((s < 10) ? "0" : "") + s.ToString();
		
		strTime = hr + ':' + min + ':' + sec;

        if(s % 25 == 0){
            updateMinTimeBtwShots(m, s);
            prevS = s;
        }
	}

    void updateMinTimeBtwShots(int m, int s) {
        if((s != 0 || m > 0 && s == 0) &&   // prevent first run
           (s != prevS)                     // prevent spamming on one secong tick
        ){
            if(TurretController.minTimeBtwShots > 0.4f) {   // lower bound
                TurretController.minTimeBtwShots -= 0.05f;
            }

            makeDeflectorShorter();
        }
    }

    void makeDeflectorShorter() {
        GameObject d = GameObject.Find("bDeflector");
        GameObject d1 = GameObject.Find("rDeflector");

        if(d.transform.localScale.x > 15) {
            d.transform.localScale -= new Vector3(0.8f, 0, 0);
            d1.transform.localScale -= new Vector3(0.8f, 0, 0);
        }

        showUpdate = true;
        updateTimer = 400;
    }
}

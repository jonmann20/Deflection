using UnityEngine;
using System.Collections;

public class GameClock : MonoBehaviour {

	//GameClock gameClock;
	float totalTime;
	int h, m, s;
	string hr, min, sec;

	void Start () {
		//gameClock = GetComponent(GameClock);
		totalTime = 0.0f;
	}

	void Update () {
		totalTime += Time.deltaTime;

		getTime();
	}

	void getTime(){
		h = (int)(totalTime / 3600);
		m = (int)(totalTime / 60);
		s = (int)(totalTime % 60);
		
		hr = ((h < 10) ? "0" : "") + h.ToString();
		min = ((m < 10) ? "0" : "") + m.ToString();
		sec = ((s < 10) ? "0" : "") + s.ToString();
		
		GetComponent<TextMesh>().text = hr + ':' + min + ':' + sec;
	}
}

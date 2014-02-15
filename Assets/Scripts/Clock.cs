﻿using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

	float totalTime;
	int h, m, s;
	string hr, min, sec;
	
	public static string strTime;
	
	void Start() {
		totalTime = 0.0f;
	}
	
	void Update() {
		totalTime += Time.deltaTime;
	}

	void OnGUI(){
		updateTimeDisplay();
	}
	
	void updateTimeDisplay(){
		h = (int)(totalTime / 3600);
		m = (int)(totalTime / 60);
		s = (int)(totalTime % 60);
		
		hr = ((h < 10) ? "0" : "") + h.ToString();
		min = ((m < 10) ? "0" : "") + m.ToString();
		sec = ((s < 10) ? "0" : "") + s.ToString();
		
		strTime = hr + ':' + min + ':' + sec;

        GUIStyle st = new GUIStyle();
        st.fontSize = 20;
        GUI.Box(new Rect(Screen.width/2 - 122/2, 3, 122, 23), strTime, st);
	}
}

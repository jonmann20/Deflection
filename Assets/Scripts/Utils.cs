﻿using UnityEngine;
using System.Collections;

public enum Turn { PLAYER, OPPONENT, OVER };
public enum Dir { NONE, UP, DOWN, LEFT, RIGHT, TOP, BOTTOM };
public enum Difficulty { EASY, MEDIUM, HARD };

public delegate void Callback ();

public class Utils : MonoBehaviour {
    public static Utils that;

    public const float FULLW = 1920;
    public const float FULLH = 1080;

    public const float HALFW = FULLW / 2;
    public const float HALFH = FULLH / 2;

    void Awake(){
        that = this;
    }

    public static void scaleGUI(){
        float rx = Screen.width / FULLW;
        float ry = Screen.height / FULLH;

        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(rx, ry, 1)); 
    }

    public static void placeTxt(string str, int fontSize, float x, float y, bool black=false) {
        GUIContent content = new GUIContent(str);

        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = black ? Color.black : Color.white;

        Vector2 size = style.CalcSize(content);
        GUI.Label(new Rect(x - size.x/2, y - size.y, size.x, size.y), content, style);
    }

    public static void blinkTxt(string str, int fontSize, float x, float y) {
        GUIContent content = new GUIContent(str);

        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = new Color(255, 255, 255, Mathf.PingPong(Time.time, 1));

        Vector2 size = style.CalcSize(content);
        GUI.Label(new Rect(x - size.x/2, y - size.y, size.x, size.y), content, style);
    }

	public IEnumerator MoveToPosition(Transform tForm, Vector3 newPos, float time, Callback callback){
        float elapsedTime = 0;
		Vector3 startingPos = tForm.position;
        
		while (elapsedTime < time){
			tForm.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
			elapsedTime += Time.deltaTime; 
			
			if(elapsedTime >= time){
                tForm.position = newPos;

                if(callback != null) {
                    callback();
                }
			}

            yield return null;
		}
	}

    public IEnumerator MoveToPosition(Camera cam, float newFieldOfView, float time, Callback callback) {
        float elapsedTime = 0;
        float initFov = cam.fieldOfView;
        //float diff = Mathf.Abs(cam.fieldOfView - newFieldOfView);

        while(elapsedTime < time) {
            cam.fieldOfView = Mathf.Lerp(initFov, newFieldOfView, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            // update the difference 
            //diff = Mathf.Abs(cam.fieldOfView - newFieldOfView);
            
            if(elapsedTime >= time) {
                cam.fieldOfView = newFieldOfView;

                if(callback != null){
                    callback();
                } 
            }

            yield return null;
        }
    }
}

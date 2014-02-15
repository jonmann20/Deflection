using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	
	void Update(){
        if(Input.GetKey(KeyCode.Return)) {
            Application.LoadLevel("main");
        }
	}

    void OnGUI(){
        Utils.that.scaleGUI();

        GUIContent titleContent = new GUIContent("tyBomber");
        GUIStyle titleStyle = new GUIStyle();
        titleStyle.fontSize = 86;
        titleStyle.normal.textColor = Color.white;
        Vector2 titleSize = titleStyle.CalcSize(titleContent);

        GUIContent ctaContent = new GUIContent("Press Enter");
        GUIStyle ctaStyle = new GUIStyle();
        ctaStyle.fontSize = 38;
        ctaStyle.normal.textColor = Color.white;
        Vector2 ctaSize = ctaStyle.CalcSize(ctaContent);
        

        GUI.Label(new Rect(Utils.HALFW - titleSize.x/2, Utils.FULLH/5 - titleSize.y, titleSize.x, titleSize.y), titleContent, titleStyle);
        GUI.Label(new Rect(Utils.HALFW - ctaSize.x/2, Utils.FULLH/1.8f - ctaSize.y, ctaSize.x, ctaSize.y), ctaContent, ctaStyle);
    }
}


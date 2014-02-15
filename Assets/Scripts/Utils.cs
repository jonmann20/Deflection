using UnityEngine;
using System.Collections;

// TODO: move to global file
public enum Turn { PLAYER, OPPONENT };
public enum Dir { EMPTY, UP, DOWN, LEFT, RIGHT, TOP, BOTTOM };

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

    public static void placeTxt(string str, int fontSize, float x, float y) {
        GUIContent content = new GUIContent(str);

        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = Color.white;

        Vector2 size = style.CalcSize(content);
        GUI.Label(new Rect(x - size.x/2, y - size.y, size.x, size.y), content, style);
    }
}

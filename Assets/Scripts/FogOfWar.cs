using UnityEngine;
using System.Collections;

public class FogOfWar : MonoBehaviour {
    Mesh mesh;
    Vector3[] vertices;
    Color[] colors;

    const float radius = 40;
    float sqrRadius = radius*radius;

    Texture2D tex;

    void Start() {


        // Create a new texture and assign it to the renderer's material
        //tex = new Texture2D(200, 100);
        //renderer.material.mainTexture = tex;
        //for (int y=0; y < tex.height; ++y) {
        //    for (int x= 0; x < tex.width; ++x) {
        //        tex.SetPixel(x, y, Color.red);
                
        //        Color color = Color.black;
        //        color.a = 1;

        //        tex.SetPixel(x, y, color);
        //    }
        //}

        //// Apply all SetPixel calls
        //tex.Apply();


        // get the mesh
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];

        for(int i=0; i < vertices.Length; ++i) {
            colors[i] = Color.yellow;
            colors[i].a = 1;
        }

        mesh.colors = colors;
        //updateFog();
    }

    void Update() {
        if(Input.GetMouseButton(0)) {
            Ray ray = GameObject.Find("MainCamera").camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
                //ChangeColor(hit.triangleIndex);
                change(hit);
            }
        }
    }


    void change(RaycastHit hit){
        // Find the u,v coordinate of the Texture
        Vector2 uv;

        uv.x = (hit.point.x - hit.collider.bounds.min.x) / hit.collider.bounds.size.x;
        uv.y = (hit.point.y - hit.collider.bounds.min.y) / hit.collider.bounds.size.y;

        // Paint it red
        //Texture2D tex = (Texture2D)hit.transform.gameObject.renderer.sharedMaterial.mainTexture;
        Color color = Color.red;
        color.a = 0;
        tex.SetPixel((int)(uv.x * tex.width), (int)(uv.y * tex.height), color);

        tex.Apply();
    }


    void ChangeColor(int iTriangle) {
        print(iTriangle);
        int iStart = mesh.triangles[iTriangle*3];
        print(iStart);

        colors[iStart].a = 0;

        //for(int i=iStart; i < iStart+4; ++i){
            //colors[i].a = 0;
        //}

        mesh.colors = colors;
    }

    void updateFog() {
        Vector3 position = new Vector3(0, 50, -9);

        for(int i=0; i < vertices.Length; ++i) {
            // getting the vertices around the local point passed
            float sqrMagnitude = (vertices[i] - position).sqrMagnitude;

            // if the vertex is within radius
            //print(sqrMagnitude + ", " + sqrRadius);
            if(sqrMagnitude <= sqrRadius) {
                print(sqrMagnitude);
                colors[i].a = 0;
            }
        }

        mesh.colors = colors;
    }

}


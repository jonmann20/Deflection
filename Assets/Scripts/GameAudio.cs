using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

    GameObject _bgMusic, _thud, _score, audioHolder;
    static AudioSource bgMusic, thud, score;
    
	void Awake(){
        audioHolder = new GameObject("_AudioHolder");
        audioHolder.AddComponent<AudioListener>();

        setSound(ref _bgMusic, ref bgMusic, "victory");
        setSound(ref _score, ref score, "enchant");
        setSound(ref _thud, ref thud, "thud");

        bgMusic.loop = true;
        bgMusic.volume = 0.11f;
	}

    void setSound(ref GameObject holder, ref AudioSource src, string clip){
        holder = new GameObject(clip);
        holder.transform.parent = audioHolder.transform;

        src = holder.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.clip = Resources.Load<AudioClip>("Audio/" + clip);
    }

    public static void play(string clip){
        switch(clip) {
            case "bgMusic":
                bgMusic.GetComponent<AudioSource>().Play();
                break;
        }
    }

    public static void playThud(Vector3 point) {
        thud.panStereo = point.x / 256;
        thud.Play();
    }

    public static void playScore(bool isLeft) {
        if(isLeft) {
            score.panStereo = -0.85f;
        }
        else {
            score.panStereo = 0.85f;
        }

        score.GetComponent<AudioSource>().Play();
    }
}

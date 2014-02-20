using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

    GameObject _bgMusic, _thud, _score, audioHolder;
    public static AudioSource bgMusic, thud, score;

	void Awake(){
        audioHolder = new GameObject("_AudioHolder");
        audioHolder.AddComponent<AudioListener>();

        setSound(ref _bgMusic, ref bgMusic, "victory");
        setSound(ref _score, ref score, "enchant");
        setSound(ref _thud, ref thud, "thud");
	}

    void setSound(ref GameObject holder, ref AudioSource src, string clip){
        holder = new GameObject(clip);
        holder.transform.parent = audioHolder.transform;

        src = holder.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.clip = Resources.Load<AudioClip>("Audio/" + clip);

        if(clip == "victory") {
            src.loop = true;
            src.volume = 0.45f;
        }
    }

    public static void play(string clip){
        switch(clip) {
            case "bgMusic":
                bgMusic.audio.Play();
                break;
            case "score":
                score.audio.Play();
                break;
            case "thud":
                thud.audio.Play();
                break;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    public static SoundManager _instance;
    private Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();
    public AudioClip[] audioClipArray;
    public AudioSource audioSource;
    public bool isQuiet = false;

    void Start() {
        foreach (AudioClip ac in audioClipArray) {
            audioDict.Add(ac.name, ac);
        }
    }


    void Awake() {
        _instance = this;
    }

    public void Play( string audioName ) {
        if (isQuiet) return;
        AudioClip ac;
        if (audioDict.TryGetValue(audioName, out ac)) {
            AudioSource.PlayClipAtPoint(ac, Vector3.zero);
            //this.audioSource.PlayOneShot(ac);
        }
    }

    public void Play(string audioName, AudioSource audioSource) {
        if (isQuiet) return;
        AudioClip ac;
        if (audioDict.TryGetValue(audioName, out ac)) {
            //AudioSource.PlayClipAtPoint(ac, Vector3.zero);
            audioSource.PlayOneShot(ac);
        }
    }

}

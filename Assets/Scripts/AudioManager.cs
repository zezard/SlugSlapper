using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Dictionary<string, AudioClip> sounds;

	// Use this for initialization
	void Start () {
        sounds = new Dictionary<string, AudioClip>();
		AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource source in sources)
        {
            sounds.Add(source.name, source.clip);
            Debug.Log(source.name);
        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

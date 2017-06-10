﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	private static AudioManager _instance;
	public static AudioManager GetInstance() {
		return _instance;
	}

    public Dictionary<string, AudioClip> sounds;
	public SlapSoundbox slapSB1;
	public SlapSoundbox slapSB2;

	//
	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
        sounds = new Dictionary<string, AudioClip>();
		AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource source in sources)
        {
            //sounds.Add(source.name, source.clip);
            //Debug.Log(source.name, source.gameObject);
        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapSoundbox : MonoBehaviour {
	public List<AudioSource> slapSounds;
	public List<AudioSource> tapSounds;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//
	public void PlaySlapSound() {
		int index = Random.Range(0, slapSounds.Count);
		//Debug.Log("playing slap sound " + index);
		slapSounds[index].Play();
	}

	//
	public void PlayPatSound() {
		int index = Random.Range(0, tapSounds.Count);
		//Debug.Log("playing slap sound " + index);
		tapSounds[index].Play();
	}
}

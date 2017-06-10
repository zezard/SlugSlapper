using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
	private static MainController _instance;
	public static MainController GetInstance() {
		return _instance;
	}

	private AudioManager _audioMgr;

	public GameObject slapFeedbackPref;

	//
	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		_audioMgr = AudioManager.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//
	public void OnSlap(SlapBox sb) {
		sb.transform.parent.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
		CreateFeedback(sb.transform.position, "Slappety Slap!");
	}

	//
	public void CreateFeedback(Vector3 pos, string text) {
		GameObject newSlapFB = Instantiate(slapFeedbackPref);
		newSlapFB.transform.position = pos;
		newSlapFB.GetComponentInChildren<Text>().text = text;
	}
}

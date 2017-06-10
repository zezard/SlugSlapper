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
	public SlapBox debugSlapper;

	//
	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		_audioMgr = AudioManager.GetInstance();
		_audioMgr.PlaySong(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("1")) {
			_audioMgr.SwitchSongLayer(0);
		}
		if(Input.GetKeyDown("2")) {
			_audioMgr.SwitchSongLayer(1);
		}
		if(Input.GetKeyDown("3")) {
			_audioMgr.SwitchSongLayer(2);
		}


		if(Input.GetKeyDown("k")) {
			OnSlap(debugSlapper, 0.3f);
		}
	}

	//
	public void OnSlap(SlapBox sb, float power) {
		if(power > 0.01 && power < 0.02) {
			sb.transform.parent.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
			CreateFeedback(sb.transform.position, "Tap");
			//sb.transform.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
		} else {
			sb.transform.parent.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
			CreateFeedback(sb.transform.position, "Slappety Slap!");
			//sb.transform.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
		}
	}

	//
	public void CreateFeedback(Vector3 pos, string text) {
		GameObject newSlapFB = Instantiate(slapFeedbackPref);
		newSlapFB.transform.position = pos;
		newSlapFB.GetComponentInChildren<Text>().text = text;
	}
}

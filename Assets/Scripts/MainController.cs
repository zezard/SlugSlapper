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
	public Transform playerHeadTransform;
	public SlapBox debugSlapper;

	public int powerSampleCount = 20;
	public float tapThresh = 0.3f;
	public float slapThresh = 0.5f;
	public float slapDelay = .2f;

	public float intensity = 0f;
	private int _curIntesityLevel = 0;

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
	void Update() {
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

		if(intensity > 0) {
			intensity -= Time.deltaTime;
		}
		if(intensity < 1 && _curIntesityLevel != 0) {
			_audioMgr.SwitchSongLayer(0);
			_curIntesityLevel = 0;
		} else if(intensity >= 2 && intensity < 5 && _curIntesityLevel != 1) {
			_audioMgr.SwitchSongLayer(1);
			_curIntesityLevel = 1;
		} else if(intensity >= 5 && _curIntesityLevel != 2) {
			_audioMgr.SwitchSongLayer(2);
			_curIntesityLevel = 2;
		}
	}

	//
	public void OnSlap(SlapBox sb, float power) {
		if(intensity < 6) {
			intensity += power * 10;
		}

		if(power > tapThresh && power < slapThresh) {
			sb.transform.parent.GetComponentInChildren<SlapSoundbox>().PlayPatSound();
			CreateFeedback(sb.transform.position, "Tappety");
			//sb.transform.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
		} else if(power > slapThresh){
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

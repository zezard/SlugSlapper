using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
	public ScoreManager scoreManager;

	private const int TAP_SCORE = 300;
	private const int SLAP_SCORE = 500;

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
	public void OnSlap(SlapBox sb, float power) {
		if(power < 0.02) {
			sb.transform.parent.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
			CreateFeedback(sb.transform.position, "Tap");
			scoreManager.AddScore ((int)(100 * power * TAP_SCORE));
		} else {
			sb.transform.parent.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
			CreateFeedback(sb.transform.position, "Slappety Slap!");
			scoreManager.AddScore ((int)(100 * power * SLAP_SCORE));
		}
	}

	//
	public void CreateFeedback(Vector3 pos, string text) {
		GameObject newSlapFB = Instantiate(slapFeedbackPref);
		newSlapFB.transform.position = pos;
		newSlapFB.GetComponentInChildren<Text>().text = text;
	}
}

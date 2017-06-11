using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
	public ScoreManager scoreManager;
	public Text timerText;

	private const int TAP_SCORE = 300;
	private const int SLAP_SCORE = 500;

	private static MainController _instance;

	public static MainController GetInstance() {
		return _instance;
	}

	private AudioManager _audioMgr;

	public GameObject slapFeedbackPref;
	public Transform playerHeadTransform;
	public SlapBox debugSlapper;

	public Animator cameraAnimator;

	public enum GameState { Start, StoryMode, Delay, Fading, Playing, GameOver };
	public GameState gameState = GameState.Start;
	public float stateTimer;

	public int powerSampleCount = 20;
	public float tapThresh = 0.3f;
	public float slapThresh = 0.5f;
	public float slapDelay = .2f;

	private float timeSinceFirstSlap = 0.0f;
	private bool hasSlappingStarted = false;
	private const float MAX_SLAPPING_TIME = 30.0f;

	public float intensity = 0f;
	private int _curIntesityLevel = 0;

	private bool isGameOver = false;

	//
	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		_audioMgr = AudioManager.GetInstance();
		//_audioMgr.PlaySong(0, 0);
	}

	// Update is called once per frame
	void Update() {
		stateTimer += Time.deltaTime;
		switch(gameState) {
			case GameState.Start:
				if(stateTimer >= 2) {
					gameState = GameState.StoryMode;
					_audioMgr.PlayStory();
					stateTimer = 0;
				}
				break;
			case GameState.StoryMode:
				if(stateTimer >= 18) {
					gameState = GameState.Delay;
					stateTimer = 0;
				}
				break;
			case GameState.Delay:
				if(stateTimer >= 2) {
					gameState = GameState.Fading;
					cameraAnimator.Play("FadeIn");
					stateTimer = 0;
					_audioMgr.PlaySong(0, 0);
				}
				break;
			case GameState.Playing:
				if (isGameOver) {
					gameState = GameState.GameOver;
				}
				UpdatePlaying();
				//if(stateTimer >= 18) {
				//	gameState = GameState.Delay;
				//	stateTimer = 0;
				//}
				break;
			case GameState.GameOver:
				break;
		}
	}

	//
	public void OnSlap(SlapBox sb, float power) {
		if (timeSinceFirstSlap > MAX_SLAPPING_TIME) {
			return;
		}

		if (!hasSlappingStarted) {
			hasSlappingStarted = true;
		}


		if(intensity < 6) {
			intensity += power * 10;
		}

		if(power > tapThresh && power < slapThresh) {
			sb.transform.parent.GetComponentInChildren<SlapSoundbox>().PlayPatSound();
			CreateFeedback(sb.transform.position, "Tappety");
			scoreManager.AddScore ((int)(100 * power * TAP_SCORE + _curIntesityLevel * 500.0f));
			//sb.transform.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
		} else if(power > slapThresh){
			sb.transform.parent.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
			CreateFeedback(sb.transform.position, "Slappety Slap!");
			scoreManager.AddScore ((int)(100 * power * SLAP_SCORE + _curIntesityLevel * 1000.0f));
			//sb.transform.GetComponentInChildren<SlapSoundbox>().PlaySlapSound();
		}
	}

	//
	public void OnAnimationOver() {
		switch(gameState) {
			case GameState.Fading:
				
				gameState = GameState.Playing;
				stateTimer = 0;
				break;
		}
	}

	//
	public void CreateFeedback(Vector3 pos, string text) {
		GameObject newSlapFB = Instantiate(slapFeedbackPref);
		newSlapFB.transform.position = pos;
		newSlapFB.GetComponentInChildren<Text>().text = text;
	}

	//
	public void UpdatePlaying() {
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

		UpdateSlapTime ();
	}

	private void UpdateSlapTime() {
		if (hasSlappingStarted) {
			timeSinceFirstSlap += Time.deltaTime;

			if (timeSinceFirstSlap >= MAX_SLAPPING_TIME) {
				isGameOver = true;
			}

			timerText.text = ((int)(MAX_SLAPPING_TIME - timeSinceFirstSlap)).ToString();
		}
	}

	//
	public void UpdateDebug() {
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
}

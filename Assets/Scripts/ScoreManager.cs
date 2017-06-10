using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public GameObject scoreObject;
	private Text scoreText;
	int score;

	public void Start() {
		scoreText = scoreObject.GetComponentInChildren<Text>();
	}

	public void Update() {
		if (scoreText.transform.localScale.x > 1.0f) {
			float shrinkFactor = 0.95f;
			scoreText.transform.localScale = new Vector2 (scoreText.transform.localScale.x - shrinkFactor * Time.deltaTime * scoreText.transform.localScale.x,
				scoreText.transform.localScale.y - shrinkFactor * Time.deltaTime * scoreText.transform.localScale.y);

			if (scoreText.transform.localScale.x < 1.0f) {
				scoreText.transform.localScale = new Vector2 (1.0f, 1.0f);
			}
		}
	}

	public int GetScore() {
		return score;
	}

	public void AddScore(int points) {
		score += points;
		UpdateScoreText ();
		scoreText.transform.localScale = new Vector2 (2.0f, 2.0f);
	}

	private void UpdateScoreText() {
		scoreText.text = score.ToString();
	}

}

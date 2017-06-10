using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	int score;

	public int GetScore() {
		return score;
	}

	public void AddScore(int points) {
		score += points;
		UpdateScoreText ();
	}

	private void UpdateScoreText() {
		scoreText = score;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
	private Transform _playerHeadTransform;

	// Use this for initialization
	void Start () {
		_playerHeadTransform = MainController.GetInstance().playerHeadTransform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(_playerHeadTransform);
	}
}

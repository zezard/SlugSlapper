using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapBox : MonoBehaviour {
	private MainController _controller;

	// Use this for initialization
	void Start () {
		_controller = MainController.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("k")) {
			_controller.OnSlap(this);
		}
	}

	//
	private void OnCollisionEnter(Collision collision) {
		_controller.OnSlap(this);
	}

	//
	//void OnCollisionTrigger(Collider collider) {
	//	_controller.OnSlap();
	//}
}

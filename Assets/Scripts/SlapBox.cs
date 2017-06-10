using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapBox : MonoBehaviour {
	private MainController _controller;
	private bool _isSlapping = false;
	private Stack<float> _distances = new Stack<float>();

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
		Debug.Log(collision.gameObject.name);
		if(collision.gameObject.GetComponent<Slappable>() && !_isSlapping){
			_controller.OnSlap(this);
			_isSlapping = true;
		}
	}

	//
	private void OnCollisionExit(Collision collision) {
		if(collision.gameObject.GetComponent<Slappable>() && _isSlapping){
			_isSlapping = false;
		}
	}

	//
	//void OnCollisionTrigger(Collider collider) {
	//	_controller.OnSlap();
	//}
}

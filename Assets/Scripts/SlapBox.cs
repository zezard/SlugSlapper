﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapBox : MonoBehaviour {
	private MainController _controller;
	private bool _isSlapping = false;
	//private Stack<float> _distances = new Stack<float>(20);
	private Queue<float> _distances = new Queue<float>();
	private Vector3 _lastPos;
	public int distSamples = 20;
	public float curMeanDist = 0f;

	// Use this for initialization
	void Start () {
		_controller = MainController.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("k")) {
			_controller.OnSlap(this, 0);
		}
		if(_lastPos == null) {
			_lastPos = transform.position;
		}

		_distances.Enqueue(Vector3.Distance(transform.position, _lastPos));
		if(_distances.Count > distSamples) {
			_distances.Dequeue();
		}

		float totalDistance = 0f;
		foreach(float dist in _distances) {
			totalDistance += dist;
		}
		curMeanDist = totalDistance / distSamples;

		_lastPos = transform.position;

	}

	//
	private void OnCollisionEnter(Collision collision) {
		Debug.Log(collision.gameObject.name);
		if(collision.gameObject.GetComponent<Slappable>() && !_isSlapping){
			_controller.OnSlap(this, curMeanDist);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapBox : MonoBehaviour {
	private MainController _controller;
	private bool _isSlapping = false;
	private int _collisionCount = 0;
	//private Stack<float> _distances = new Stack<float>(20);
	private Queue<float> _distances = new Queue<float>();
	private Vector3 _lastPos;
	//public int distSamples = 20;
	public float curMeanDist = 0f;
	private float _slapDelay = 0f;

	// Use this for initialization
	void Start () {
		_controller = MainController.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
		if(_lastPos == null) {
			_lastPos = transform.position;
		}

		_distances.Enqueue(Vector3.Distance(transform.position, _lastPos));
		if(_distances.Count > _controller.powerSampleCount) {
			_distances.Dequeue();
		}

		float totalDistance = 0f;
		foreach(float dist in _distances) {
			totalDistance += dist;
		}
		curMeanDist = totalDistance / _controller.powerSampleCount;

		_lastPos = transform.position;

		if(_slapDelay > 0) {
			_slapDelay -= Time.deltaTime;
		}
	}

	//
	private void OnCollisionEnter(Collision collision) {
		//Debug.Log(collision.gameObject.name);
		if(collision.gameObject.GetComponent<Slappable>()){
			if(_slapDelay <= 0 && _collisionCount == 0) {
				_controller.OnSlap(this, curMeanDist);
				_slapDelay = _controller.slapDelay;
			}
			_collisionCount++;
		}
	}

	//
	private void OnCollisionExit(Collision collision) {
		if(collision.gameObject.GetComponent<Slappable>()){
			_collisionCount--;
		}
	}

	//
	//void OnCollisionTrigger(Collider collider) {
	//	_controller.OnSlap();
	//}
}

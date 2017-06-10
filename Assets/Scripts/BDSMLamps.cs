using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDSMLamps : MonoBehaviour {
	private MainController _controller;

	public Color color1;
	public Color color2;
	public Light light;
	private float _intensityDiff = 1;
	private float _intensityThresh = 1;

	// Use this for initialization
	void Start () {
		_controller = MainController.GetInstance();

		light = GetComponent<Light>();
		_intensityThresh = Random.Range(0.1f, 1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		light.intensity += _intensityDiff * Time.deltaTime * _controller.intensity;
		if(_intensityDiff > 0 && light.intensity > _intensityThresh) {
			_intensityThresh = Random.Range(0.6f, 0.8f);
			_intensityDiff = -1;
		} else if(_intensityDiff < 0 && light.intensity < _intensityThresh) {
			_intensityThresh = Random.Range(0.9f, 1.1f);
			_intensityDiff = 1;
		}
	}
}

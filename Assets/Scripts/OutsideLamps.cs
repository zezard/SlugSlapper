using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideLamps : MonoBehaviour {
	public Color color1;
	public Color color2;
	public Light light;
	private float _switchTimer = 0;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		_switchTimer = Random.Range(1f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		_switchTimer -= Time.deltaTime;
		if(_switchTimer <= 0) {
			if(light.intensity == 0) {
				light.intensity = 1;
				_switchTimer = Random.Range(1f, 10f);
			} else {
				light.intensity = 0;
				_switchTimer = Random.Range(0.05f, 0.5f);
			}
			
		}
	}
}

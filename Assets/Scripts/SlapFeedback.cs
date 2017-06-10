using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapFeedback : MonoBehaviour {
	private float _lifeTime = 0f;
	public float lifeTime = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		_lifeTime += Time.deltaTime;
		if(_lifeTime > lifeTime) {
			Destroy(gameObject);
		}
		transform.position += transform.up * 1 * Time.deltaTime;
	}
}

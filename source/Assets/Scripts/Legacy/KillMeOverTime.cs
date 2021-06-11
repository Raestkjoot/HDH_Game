using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMeOverTime : MonoBehaviour {

	private float killTimer;
	public float lifeSpan;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeSpan <= killTimer) {
			Destroy (gameObject);
		}
		killTimer += Time.deltaTime;
	}
}

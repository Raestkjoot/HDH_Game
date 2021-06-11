using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingTools : MonoBehaviour {

	public GameObject[] tools;
	private int currentTool;
	public GameObject movementScript;

	// Use this for initialization
	void Start () {
		//start with just hands.
		tools [0].SetActive (false); //0 = sword
		tools [1].SetActive (false); //1 = pickaxe
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			//Toll 1: The sword
			tools [0].SetActive (true);
			tools [1].SetActive (false);
			movementScript.GetComponent<BasicMovementScript> ().swordDrawn = true;

		}
		if (Input.GetKeyUp (KeyCode.Alpha2)) {
			//Tool 2: The pickaxe
			tools [0].SetActive (false);
			tools [1].SetActive (true);
			movementScript.GetComponent<BasicMovementScript> ().swordDrawn = false;
		}
		if (Input.GetKeyUp (KeyCode.Alpha3)) {
			//Hands only
			tools [0].SetActive (false);
			tools [1].SetActive (false);
			movementScript.GetComponent<BasicMovementScript> ().swordDrawn = false;
		}
	}
}

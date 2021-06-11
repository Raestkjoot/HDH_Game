using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCounter : MonoBehaviour {

	public int goldAmount = 0;
	public TextMesh text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void UpdateGold(int amount){
		goldAmount += amount;
		text.text = "gold: " + goldAmount;
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mining : MonoBehaviour {

	public bool isClose = false;
	public Transform goldVein;
	public Transform goldCounter;
	public float curHealth = 100;
	public float maxHealth = 100;
	public float damage = 30;
	private float startingSize;

	// Use this for initialization
	void Start () {
		startingSize = goldVein.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (isClose == true) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				curHealth -= damage;
					if(curHealth < 0) {
						curHealth = 0;
					}
					if(curHealth == 0) {
					goldCounter.GetComponent <GoldCounter>().UpdateGold(50);
						Destroy(gameObject);
					}
					if(curHealth > 0) {
					float newSize = curHealth / maxHealth * startingSize;
					goldVein.localScale = new Vector3(newSize,newSize,newSize);
					}
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		isClose = true;
	}
	void OnTriggerExit (Collider other)
	{
		isClose = false;
	}
}

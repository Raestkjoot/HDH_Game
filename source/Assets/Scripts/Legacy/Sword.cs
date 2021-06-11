using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public GameObject blood;
	public GameObject critBlood;

	public bool isPlayer = false;
	public int damage;
	public int counterDamage;
	public bool isCountering = false;
	public bool isSwinging = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		//print ("hit: " + other.name);
		if (isPlayer == false) {
			if (other.tag == "Player" && isSwinging == true) {
				other.GetComponent<PlayerCombat> ().curHealth -= damage;
				other.GetComponent<PlayerCombat> ().canCounter = false;
				isSwinging = false;
				//print ("dealt damage to player!");
				Instantiate (blood, other.transform.position, other.transform.rotation);

			}
		}
		else {
			if (other.tag == "Enemy") {
				if (isSwinging == true) {
					if (isCountering == false) {
						other.GetComponentInParent<EnemyAI> ().curHealth -= damage;
						Instantiate (blood, other.transform.position, other.transform.rotation);
					}
					else {
						other.GetComponentInParent<EnemyAI> ().curHealth -= counterDamage;
						Instantiate (critBlood, other.transform.position, other.transform.rotation);
						isCountering = false;
						//dealt bonus counter damage!
					}
					isSwinging = false;
					//dealt damage to enemy!
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public int curHealth;
	public int maxHealth;

	public Transform target;
	public Transform player;
	public Animator animator;
	public GameObject sword;
	public GameObject corpse;

	private float dist;
	public float rotSpeed;

	private bool attacking = false;
	public int whatAttack;
	private float attackTimer;
	public float minAttackCooldown, maxAttackCooldown;
	public float dodgeWindow, attackFinishTime;

	public int playersAttack = 0;
	public int dodgeChance = 70;

	//private bool didDodge = false;
	public bool fightMode = false;

	// Use this for initialization
	void Start () {
		curHealth = maxHealth;
		target = player;
		attackTimer = Random.Range (minAttackCooldown, maxAttackCooldown);
	}
	
	// Update is called once per frame
	void Update () {

		if (curHealth <= 0) {
			Instantiate (corpse, transform.position, transform.rotation);
			Destroy (gameObject);
		}

		if (fightMode == true) {
			//Rotate to face the player.
			Vector3 targetDir = target.position - transform.position;
			targetDir.y = 0.0f;
			if (attacking == false) {
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (targetDir), Time.deltaTime * rotSpeed);
			}

			//Start attacking the player when he is in range.
			dist = targetDir.magnitude;
			if (dist < 3.5) {
				if (attackTimer <= 0) {
					attackTimer = (dodgeWindow + attackFinishTime + Random.Range (minAttackCooldown, maxAttackCooldown));
					StartCoroutine (Attack ());
				}
			}

			if (playersAttack != 0) {
				StartCoroutine (Dodge ());
			}
		}
		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		}
	}

	IEnumerator Attack (){
		attacking = true;
		//from 1 to 3, L M or R attack, 0 is idle.
		whatAttack = Random.Range (1, 4);
		switch (whatAttack) {
		case 1:
			//Right attack, going from enemy's right to left
			animator.SetInteger ("Attack", 1);
			break;
		case 2:
			//Mid attack, going down the center line
			animator.SetInteger ("Attack", 2);
			break;
		case 3:
			//Left attack, going from enemy's left to right
			animator.SetInteger ("Attack", 3);
			break;
		}

		yield return new WaitForSeconds (dodgeWindow);

		sword.GetComponent<Sword> ().isSwinging = true;
		animator.SetInteger ("Attack", 0);

		yield return new WaitForSeconds (attackFinishTime);

		sword.GetComponent<Sword> ().isSwinging = false;
		attacking = false;
		whatAttack = 0;
		//didDodge = false;

	}

	IEnumerator Dodge (){
		if (attacking == true) {
		}
		else {
			if (Random.Range (0, 100) < dodgeChance) {
				switch (playersAttack) {
				case 1:
					animator.SetInteger ("Dodge", 3);
					break;
				case 2:
					animator.SetInteger ("Dodge", 2);
					break;
				case 3:
					animator.SetInteger ("Dodge", 1);
					break;
				default:
					Debug.LogError ("dodge error");
					break;
				}

			}
			playersAttack = 0;
		}

		yield return new WaitForSeconds(0.2f);

		animator.SetInteger ("Dodge", 0);
	}
}

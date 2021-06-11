using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCombatScript : MonoBehaviour {

	public Transform enemy;
	public Transform player;
	public Transform rotatePoint;

	public Animator animator;
	public Animator playerAnimator;

	public float animTime;
	public float animLength;
	public float dodgeTime = 0.25f;
	public float dodgeSpeed;
	private float dodgeTimer;
	private int dodgeDir = 0;

	private bool didAvoid = false;
	public float avoidWindow;

	//cool down between attacks, does not include time it takes to attack.
	public float minAttackCooldown;
	public float maxAttackCooldown;
	public float attackFinishTime;
	private float attackTimer;
	private int whatAttack;

	// Use this for initialization
	void Start () {
		animator.SetFloat ("MySpeed", animLength / animTime);
		enemy = GameObject.FindWithTag ("Enemy").transform;
		player.SetParent (null);
		rotatePoint.position = enemy.position;
		player.SetParent (rotatePoint);
	}
	
	// Update is called once per frame
	void Update () {
		if (attackTimer <= 0) {
			attackTimer = (avoidWindow + attackFinishTime + Random.Range (minAttackCooldown, maxAttackCooldown));
			StartCoroutine(Attack());
		}
		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.Mouse0)) {
			if (Input.GetKeyDown (KeyCode.A)) {
				playerAnimator.SetInteger ("DodgeDir", 1);
				print ("jump left");
				didAvoid = Avoid (1);
				dodgeTimer = dodgeTime;
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				playerAnimator.SetInteger ("DodgeDir", 2);
				print ("jump back");
				didAvoid = Avoid (2);
				dodgeTimer = dodgeTime;
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				playerAnimator.SetInteger ("DodgeDir", 3);
				print ("jump right");
				didAvoid = Avoid (3);
				dodgeTimer = dodgeTime;
			}
			if (Input.GetKeyDown (KeyCode.W)) {
				print ("jump forth");
				didAvoid = Avoid (4);
				dodgeTimer = dodgeTime;
			}
		}

		if (dodgeTimer > 0) {
			dodgeTimer -= Time.deltaTime;
			switch (dodgeDir) {
			case 1:
				rotatePoint.Rotate(Vector3.up * Time.deltaTime * dodgeSpeed);
				break;
			case 2:
				player.Translate(Vector3.forward * Time.deltaTime * 10);
				break;
			case 3:
				rotatePoint.Rotate(Vector3.up * Time.deltaTime * (-dodgeSpeed));
				break;
			case 4:
				player.Translate (Vector3.forward * Time.deltaTime * (-10));
				break;				
			default:
				Debug.LogWarning ("dodge error!");
				break;
			}
		}
		else {
			playerAnimator.SetInteger ("DodgeDir", 0);
		}
	}

	IEnumerator Attack() {
		//from 1 to 3, L M or R attack, 0 is idle.
		whatAttack = Random.Range (1, 4);
		switch (whatAttack) {
		case 1:
			print ("L attack");
			animator.SetInteger ("Attack", 1);
			break;
		case 2:
			print ("M attack");
			animator.SetInteger ("Attack", 2);
			break;
		case 3:
			print ("R attack");
			animator.SetInteger ("Attack", 3);
			break;
		}
		yield return new WaitForSeconds (avoidWindow);

		animator.SetInteger ("Attack", 0);

		if (didAvoid == false) {
			switch (whatAttack) {
			case 1:
				print ("L hit!");
				break;
			case 2:
				print ("M hit!");
				break;
			case 3:
				print ("R hit!");
				break;
			}
		}
		else {
			switch (whatAttack) {
			case 1:
				print ("L avoided!");
				break;
			case 2:
				print ("M avoided!");
				break;
			case 3:
				print ("R avoided!");
				break;
			}
		}

		yield return new WaitForSeconds (attackFinishTime);
	
		whatAttack = 0;
		didAvoid = false;
	}

	bool Avoid (int dir) {
		dodgeDir = dir;
		if (dir == whatAttack) {
			return true;
		}
		else {
			return false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour {


	public int curHealth;
	public int maxHealth;

	public float attackTime;

	public float dodgeTime;
	public float dodgeCooldown;
	public float dodgeMoveSpeed;
	private float dodgeTimer;
	private int dodgeDir;

	public Animator animator;
	public GameObject sword;
	public Transform Aesthetics;
	public GameObject enemy;

	public bool canCounter = false;
	private bool canAttack = true;

	// Use this for initialization
	void Start () {
		curHealth = maxHealth;
	}

	// Update is called once per frame
	void Update () {

		if (curHealth <= 0) {
			string currentScene = SceneManager.GetActiveScene ().name;
			SceneManager.LoadScene ("" + currentScene, LoadSceneMode.Single);
		}


		//Attack with LMB
		if (Input.GetKeyDown (KeyCode.Mouse0) && canAttack == true) {
			if (enemy != null) {
				canAttack = false;
				Vector3 targetDir = enemy.transform.position - Aesthetics.position;
				float angle = Vector3.Angle (targetDir, transform.right);

				StartCoroutine (Attack (angle));
			}
			else
			{
				print ("no enemy");
			}
		}

		//Dash by holding RMB and pressing a direction (wasd)
		if (enemy != null) {
			if (Input.GetKey (KeyCode.Mouse1) && dodgeTimer <= 0) {
				if (Input.GetKey (KeyCode.A)) {
					//playerAnimator.SetInteger ("DodgeDir", 1);
					dodgeTimer = (dodgeTime + dodgeCooldown);
					dodgeDir = 1;
					if (dodgeDir == enemy.GetComponent<EnemyAI> ().whatAttack) {
						canCounter = true;
					}
				}
				if (Input.GetKey (KeyCode.S)) {
					//playerAnimator.SetInteger ("DodgeDir", 2);
					dodgeTimer = (dodgeTime + dodgeCooldown);
					dodgeDir = 2;
					if (dodgeDir == enemy.GetComponent<EnemyAI> ().whatAttack) {
						canCounter = true;
					}
				}
				if (Input.GetKey (KeyCode.D)) {
					//playerAnimator.SetInteger ("DodgeDir", 3);
					dodgeTimer = (dodgeTime + dodgeCooldown);
					dodgeDir = 3;
					if (dodgeDir == enemy.GetComponent<EnemyAI> ().whatAttack) {
						canCounter = true;
					}
				}
				if (Input.GetKey (KeyCode.W)) {
					//playerAnimator.SetInteger ("DodgeDir", 4);
					dodgeTimer = (dodgeTime + dodgeCooldown);
					dodgeDir = 4;
					if (dodgeDir == enemy.GetComponent<EnemyAI> ().whatAttack) {
						canCounter = true;
					}
				}
			}
		}

		//Move player in a dashing manner in the desired direction.
		if (dodgeTimer > 0) {
			if (dodgeTimer > (dodgeCooldown - dodgeTime)) {
				switch (dodgeDir) {
				case 1:
				//Dash left
					transform.Translate (Vector3.left * Time.deltaTime * dodgeMoveSpeed);
					break;
				case 2:
				//Dash back
					transform.Translate (Vector3.back * Time.deltaTime * dodgeMoveSpeed);
					break;
				case 3:
				//Dash right
					transform.Translate (Vector3.right * Time.deltaTime * dodgeMoveSpeed);
					break;
				case 4:
				//Dash forth
					transform.Translate (Vector3.forward * Time.deltaTime * dodgeMoveSpeed);
					break;				
				default:
					Debug.LogWarning ("dodge error!");
					break;
				}
			}
			dodgeTimer -= Time.deltaTime;
		}
	}

	IEnumerator Attack (float ang){
		if (ang < 80) {
			//Strike leftwards
			animator.SetInteger ("Attack", 1);
			enemy.GetComponent<EnemyAI> ().playersAttack = 1;
		} else if (ang < 100) {
			//Strike forwards
			animator.SetInteger ("Attack", 2);
			enemy.GetComponent<EnemyAI> ().playersAttack = 2;
		} else if (ang > 100) {
			//Strike rightwards
			animator.SetInteger ("Attack", 3);
			enemy.GetComponent<EnemyAI> ().playersAttack = 3;
		}
		if (canCounter == true) {
			sword.GetComponent<Sword> ().isCountering = true;
		}
		//is swinging
		sword.GetComponent<Sword> ().isSwinging = true;

		yield return new WaitForSeconds (attackTime);

		animator.SetInteger ("Attack", 0);
		sword.GetComponent<Sword> ().isSwinging = false;
		canCounter = false;
		//is not swinging
		canAttack = true;

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour {

	[SerializeField]

	Transform player;
	public GameObject playerCombatScript;

	NavMeshAgent _navMeshAgent;

	public float fightDist = 2;
	public int engageDist = 10;
	public bool isFighting = false;

	// Use this for initialization
	void Start () {
		_navMeshAgent = this.GetComponent<NavMeshAgent> ();

		if (_navMeshAgent == null) {
			Debug.LogError ("Nav mesh agent missing on " + gameObject.name);
		}
	}

	void Update () {

		if ((player.position - transform.position).magnitude < engageDist) {
			if (isFighting == false) {
				playerCombatScript.GetComponent<PlayerCombat> ().enemy = gameObject;
				gameObject.GetComponent<EnemyAI> ().fightMode = true;
				isFighting = true;
			}
			Vector3 target = player.position;
			target.x -= Mathf.Sign ((player.position.x - transform.position.x)) * fightDist;
			target.z -= Mathf.Sign ((player.position.z - transform.position.z)) * fightDist;

			Vector3 targetVector = transform.position;
			if (Mathf.Abs (transform.position.x - target.x) > fightDist) {
				targetVector.x = target.x;
			}
			if (Mathf.Abs (transform.position.z - target.z) > fightDist) {
				targetVector.z = target.z;
			}
			_navMeshAgent.SetDestination (targetVector);
		}
		else if (isFighting == true) {
			isFighting = false;
			playerCombatScript.GetComponent<PlayerCombat> ().enemy = null;
			gameObject.GetComponent<EnemyAI> ().fightMode = false;
		}
	}
}

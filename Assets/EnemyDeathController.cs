﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathController : MonoBehaviour {

	HealthController enemyHealth;

	void Start(){
		enemyHealth = GetComponent<HealthController> ();
	}

	void OnTriggerEnter(Collider c){
		if (enemyHealth.health <= 0) {
			GameObject tempParticle = Spawner.Spawn ("EnemyDeathParticles");
			tempParticle.transform.position = gameObject.transform.position;
			gameObject.SetActive (false);
			AudioManager.PlayEffect (AudioManager.instance.clips [Random.Range (0, 20)]);
		}

	}

	void OnCollisionEnter(Collision c){
		if (enemyHealth.health <= 0) {
			gameObject.SetActive (false);
		}

	}

}

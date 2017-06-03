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
			gameObject.SetActive (false);
		}

	}

	void OnCollisionEnter(Collision c){
		if (enemyHealth.health <= 0) {
			gameObject.SetActive (false);
		}

	}

}
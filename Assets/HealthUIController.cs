﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour {

	public Gradient grad;
	private Slider slider;

	void Start(){
		slider = GetComponent<Slider> ();
		HealthController.onAnyHealthChanged += UpdateBar;
		
	}

	void UpdateBar(HealthController healthController, float health, float prevHealth, float maxHealth){
		if (healthController.gameObject.tag == "Player") {
			float pct = health / maxHealth;
			slider.value = pct;
		}
	}
}

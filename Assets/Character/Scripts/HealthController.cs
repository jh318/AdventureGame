using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

	public delegate void OnHealthChanged(float health, float prevHealth, float maxHealth);
	public event OnHealthChanged onHealthChanged = delegate {};

	public delegate void OnAnyHealthChanged(HealthController healthController, float health, float prevHealth, float maxHealth);
	public static event OnAnyHealthChanged onAnyHealthChanged = delegate {};

	public float maxHealth = 10;
	public float health;

	void Start(){
		health = maxHealth;
	}

	public void TakeDamage(float damage){
		float prevHealth = health;
		health -= damage;
		onHealthChanged (health, prevHealth, maxHealth);
		onAnyHealthChanged (this, health, prevHealth, maxHealth);
	}
}

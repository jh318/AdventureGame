using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

	public int damage = 10;

	void OnCollisionEnter(Collision c){
		HealthController h = c.gameObject.GetComponent<HealthController> ();
		if (h != null ) h.TakeDamage (damage);
	}

	void OnTriggerEnter(Collider c){
		gameObject.SetActive (false);
		HealthController h = c.gameObject.GetComponent<HealthController> ();
		if (h != null ) h.TakeDamage (damage);

	}
}

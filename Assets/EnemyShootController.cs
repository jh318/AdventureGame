using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootController : MonoBehaviour {

	public GameObject projectile;



	void Start(){
		TripleShot ();
	}

	void TripleShot(){
		int count = 3;
		float speed = 3.0f;
		for (int i = 0; i < count; ++i) {
			float frac = i / (count - 1);
			float angle = Mathf.Lerp (-45.0f, 45.0f, frac);
			GameObject bullet = Instantiate (projectile);
			bullet.transform.position = transform.position;
			bullet.transform.forward = Quaternion.AngleAxis (angle, Vector3.up) * transform.forward;
			//Rigidbody body = bullet.GetComponent<rigidbody> ();
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;
		}
	}
}

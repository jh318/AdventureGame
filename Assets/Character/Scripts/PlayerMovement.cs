using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody body;

	void Start(){
		body = GetComponent<Rigidbody> ();

	}
}

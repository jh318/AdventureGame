using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float speed = 1.0f;

	
	// Update is called once per frame
	void Update () {
		if (target == null) return;

		transform.position = Vector3.Lerp
			(
			transform.position,
			target.position + offset,
			Time.deltaTime - speed
		);
		transform.LookAt (target);
	}
}

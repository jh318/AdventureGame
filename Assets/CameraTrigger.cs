using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

	public Transform cameraTransform;

	void OnTriggerEnter()
	{
		Camera.main.transform.position = cameraTransform.position;
		Camera.main.transform.rotation = cameraTransform.rotation;

	}
}

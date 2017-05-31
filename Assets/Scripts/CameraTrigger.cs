using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

	public Transform cameraTransform;
	public float fieldOfView;
	//public float viewportRectX;
	//public float viewportRectY;
	//public float viewportRectW;
	//public float viewportRectH;

	private CameraIsoController mainCameraScript;
	private Camera cameraComponent;

	void Start(){
		mainCameraScript = Camera.main.GetComponent<CameraIsoController> ();
		cameraComponent = Camera.main.GetComponent<Camera> ();
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player") {
			mainCameraScript.enabled = false;
			Camera.main.transform.position = cameraTransform.position;
			Camera.main.transform.rotation = cameraTransform.rotation;
			cameraComponent.fieldOfView = fieldOfView;
			//cameraComponent.rect.x = viewportRectX;
			//cameraComponent.rect.y = viewportRectY;
			//cameraComponent.rect.width = viewPort
		}
	}
}

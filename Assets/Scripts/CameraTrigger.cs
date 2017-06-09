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
	public float transitionTime = 3;

	private CameraTricks cameraTricks;
	private CameraIsoController mainCameraScript;
	private Camera cameraComponent;

	void Start(){
		mainCameraScript = Camera.main.GetComponent<CameraIsoController> ();
		cameraComponent = Camera.main.GetComponent<Camera> ();
		cameraTricks = Camera.main.GetComponent<CameraTricks> ();
	}

	void OnTriggerEnter(Collider c)
	{
		Debug.Log ("CameraTrigger");
		if (c.tag == "Player") {
			Debug.Log ("Camera Switch");
			mainCameraScript.enabled = false;
			cameraTricks.Move(cameraTransform, transitionTime);
			//Camera.main.transform.position = cameraTransform.position;
			//Camera.main.transform.rotation = cameraTransform.rotation;
			cameraComponent.fieldOfView = fieldOfView;
			//cameraComponent.rect.x = viewportRectX;
			//cameraComponent.rect.y = viewportRectY;
			//cameraComponent.rect.width = viewPort
		}
	}


}

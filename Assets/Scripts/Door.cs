using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	Animator animator;
	bool isColliding;
	public GameObject door;

	void Start()
	{
		
		animator = GetComponentInChildren<Animator> ();
	}
		
	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "Player")
			isColliding = true;
	}

	void OnTriggerExit(Collider c)
	{
		if (c.gameObject.tag == "Player")
			isColliding = false;
	}

	void Update()
	{
		ToggleDoor ();
	}
		

	void ToggleDoor()
	{
		if (isColliding && Input.GetKeyDown (KeyCode.X))
		{	
			if (door.GetComponent<MeshRenderer> ().enabled == false) {
				door.GetComponent<MeshRenderer>().enabled = true;
				door.GetComponent<BoxCollider> ().enabled = true;
			}
			//animator.SetTrigger ("ToggleDoor");
			else if(door.GetComponent<MeshRenderer>().enabled == true){
				door.GetComponent<MeshRenderer>().enabled = false;
				door.GetComponent<BoxCollider> ().enabled = false;
			}

		
		}
	}

	public void UnlockDoor(){
		door.GetComponent<MeshRenderer>().enabled = false;
		door.GetComponent<BoxCollider> ().enabled = false;
	}
}

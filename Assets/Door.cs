using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	public Text textBox;
	public Text promptBox;

	public string textBoxString;
	public string promptBoxString;

	Animator animator;


	void Start()
	{
		textBox.gameObject.SetActive(false);
		promptBox.gameObject.SetActive(false);
		animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.GetComponent<PlayerController>())
		{
			Debug.Log ("Door Works");
			promptBox.text = promptBoxString;
			promptBox.gameObject.SetActive(true);
		}
	}
		
	void OnTriggerExit(Collider c)
	{
		textBox.gameObject.SetActive(false);
		textBox.gameObject.SetActive(false);
	}

	void Update()
	{
		DoorInputCheck ();
	}



	void DoorInputCheck()
	{
		if (promptBox.gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.X)) 
		{
			textBox.text = textBoxString;
			animator.SetTrigger ("OpenDoorTrigger");
			textBox.gameObject.SetActive (true);
			promptBox.gameObject.SetActive (false);
		}

		else if (textBox.gameObject.activeSelf == true && Input.GetKeyDown (KeyCode.X)) 
		{
			//CloseDoor ();
			textBox.gameObject.SetActive (false);
			promptBox.gameObject.SetActive (true);
		}
	}

	void OpenDoor()
	{
		transform.rotation = Quaternion.AngleAxis (90, Vector3.left);
	}

	void CloseDoor()
	{
		transform.rotation = Quaternion.AngleAxis (90, Vector3.left);
	}
}

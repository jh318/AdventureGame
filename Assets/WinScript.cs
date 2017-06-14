using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		if (c.tag == "Player") {
			Debug.Log ("WIN");
			PlayerController.instance.gameOver = true;
			TextManager.instance.textBox.text = "That's it! Thanks for Playing! R to Restart";
			TextManager.instance.textBox.gameObject.SetActive (true);

		}
	}
}

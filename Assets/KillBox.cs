using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillBox : MonoBehaviour {

	public string deathMessage = "Game Over";
	public Text textBox;

	void Start(){
		textBox = TextManager.instance.textBox;
	}

	void OnTriggerEnter(Collider c){
		if (c.tag == "Player") {
			c.gameObject.SetActive (false);
			textBox.text = deathMessage;
			textBox.gameObject.SetActive (true);
		}
	}
}

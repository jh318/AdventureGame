using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUnlock : MonoBehaviour {

	//Create List of enemies
	//if list is enemy, open door

	public List<GameObject> enemies = new List<GameObject>();
	Door doorScript;

	void Start(){
		doorScript = GetComponent<Door> ();
	}

	void Update(){
		for (int i = 0; i < enemies.Count; i++) {
			if (enemies [i].activeSelf) {
				break;
			} else if(i == enemies.Count-1 && !enemies[i].activeSelf){
				Debug.Log ("UNLOCK");
				doorScript.UnlockDoor ();
			}
		} 
	}
}
	//Check if Enemies are disabled
	//Disable Door


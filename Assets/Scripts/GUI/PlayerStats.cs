using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	void OnGUI() {
		//GUI.Box(new Rect(10,10,100,50), "Player");
		GUILayout.BeginArea(new Rect(10,10,120,70), "Player", GUI.skin.window);
		GUI.Label(new Rect(15,20,90,40), "Current floor: " + GameController.Instance.currentFloor);
		GUI.Label(new Rect(15,40,90,40), "Health: " + PlayerController.Instance.health);
		GUILayout.EndArea();
	}
}

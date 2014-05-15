using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
	public GameObject nextRoom;
	public GameObject nextRoomDoor;
	public bool isLocked = false;
	// Use this for initialization
	void Start () {

	}

	void FixedUpdate () {
		//if within the trigger and player presses interaction button, enter room associated with this

	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.transform.tag == "Player") {
			if(Input.GetButtonUp("Interact") && !isLocked) {
				GameController.Instance.currentRoom = nextRoom;
				Debug.Log("next room: " + nextRoom);
				MainCameraController.Instance.updateCamera();
				other.transform.position = nextRoomDoor.transform.position;
			}
		}
	}
}

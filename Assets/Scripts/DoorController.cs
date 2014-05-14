using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
	public GameObject nextRoom;
	public GameObject nextRoomDoor;
	public bool isLocked;
	// Use this for initialization
	void Start () {

	}

	void FixedUpdate () {
		//if within the trigger and player presses interaction button, enter room associated with this

	}

	void OnTriggerStay2D(Collider2D other) {
		Debug.Log("oops!");
		if(other.transform.tag == "Player") {
			if(Input.GetButtonUp("Interact")) {
				other.transform.position = nextRoom.transform.FindChild("room_entrance").FindChild("door").position;
				GameController.Instance.currentRoom = nextRoom;
				MainCameraController.Instance.updateCamera();
			}
		}
	}
}

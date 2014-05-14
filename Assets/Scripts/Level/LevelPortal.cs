using UnityEngine;
using System.Collections;

public class LevelPortal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.transform.tag == "Player") {
			if(Input.GetButtonUp("Interact")) {
				//disable camera and player
				MainCameraController.Instance.gameObject.SetActive(false);
				PlayerController.Instance.gameObject.SetActive(false);

				//destroy rooms
				foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room")) {
					Destroy(room);
				}

				//reset level variables
				GameController.Instance.isExitPresent = false;
				GameController.Instance.currentX = 0;

				//create new rooms
				GameController.Instance.currentRoom = GameController.Instance.GenerateEntryRoom();

				MainCameraController.Instance.gameObject.SetActive(true);

				//reset player position
				PlayerController.Instance.gameObject.SetActive(true);
				PlayerController.Instance.transform.position = new Vector3(-0.02448663f, 0.3689732f, PlayerController.Instance.transform.position.z);
				//update camera
				MainCameraController.Instance.transform.position = new Vector3(-0.02448663f, 0.3689732f, MainCameraController.Instance.transform.position.z);
				MainCameraController.Instance.updateCamera();

				GameController.Instance.currentFloor++;
				//MainCameraController.Instance.transform.position = new Vector3(4.4f, 1.758484f, MainCameraController.Instance.transform.position.z);
			}
		}
	}

	//MainCameraController.Instance.gameObject.SetActive(false);
}

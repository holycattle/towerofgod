using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {
	private static Dictionary<string, Vector2> roomAreas = new Dictionary<string, Vector2>();
	//TODO: spawnEnergy should also be determined by the room area-- obviously, smaller rooms should have less enemies
	public float spawnEnergy;

	//divergent room: 36x16
	//entry room: 36x16
	//exit room 24x16
	//fountain room 20x16
	//trap room 24x16
	public Vector2 area;

	void Awake() {
		if(roomAreas.Count == 0) {
			roomAreas.Add("DivergentRoom(Clone)", new Vector2(36*.32f, 16*.32f));
			roomAreas.Add("EntryRoom(Clone)", new Vector2(36*.32f, 16*.32f));
			roomAreas.Add("ExitRoom(Clone)", new Vector2(24*.32f, 16*.32f));
			roomAreas.Add("FountainRoom(Clone)", new Vector2(20*.32f, 16*.32f));
			roomAreas.Add("TrapRoom(Clone)", new Vector2(24*.32f, 16*.32f));
			roomAreas.Add("SmallTerminalRoom(Clone)", new Vector2(20*.32f, 16*.32f));
			roomAreas.Add("LargeTerminalRoom(Clone)", new Vector2(32*.32f, 16*.32f));
		}

		Debug.Log(transform.name);
		area = roomAreas[transform.name];
		spawnEnergy = Random.Range((GameController.Instance.currentFloor * 2f), (GameController.Instance.currentFloor * 4f));
		Debug.Log("ener: " + spawnEnergy);
	}

	// Use this for initialization
	void Start () {

		foreach(Transform t in transform) {
			if(t.name == "room_exit") {
				DoorController d = t.gameObject.GetComponentInChildren<DoorController>();
				Debug.Log(d);
				if(!GameController.Instance.isExitPresent) {
					Debug.Log("creating!");
					d.nextRoom = GameController.Instance.GenerateNewRoom(gameObject);
					//point next room's entrance to this room
					d.nextRoom.transform.FindChild("room_entrance").GetComponentInChildren<DoorController>().nextRoom = gameObject;
					d.nextRoomDoor = d.nextRoom.transform.FindChild("room_entrance").FindChild("door").gameObject;

					//point nextRoomEntrance to this room
					GameObject nextRoomEntranceDoor = d.nextRoom.transform.FindChild("room_entrance").FindChild("door").gameObject;
					DoorController dc = nextRoomEntranceDoor.GetComponent<DoorController>();
					dc.nextRoom = gameObject;
					dc.nextRoomDoor = transform.FindChild("room_exit").FindChild("door").gameObject;

					Debug.Log(d);
				} else {
					//if there are more exits even though the exit is already present, create terminal rooms
					d.nextRoom = GameController.Instance.GenerateTerminalRoom();
					//point next room's entrance to this room
					d.nextRoom.transform.FindChild("room_entrance").GetComponentInChildren<DoorController>().nextRoom = gameObject;
					d.nextRoomDoor = d.nextRoom.transform.FindChild("room_entrance").FindChild("door").gameObject;
					
					//point nextRoomEntrance to this room
					GameObject nextRoomEntranceDoor = d.nextRoom.transform.FindChild("room_entrance").FindChild("door").gameObject;
					DoorController dc = nextRoomEntranceDoor.GetComponent<DoorController>();
					dc.nextRoom = gameObject;
					dc.nextRoomDoor = transform.FindChild("room_exit").FindChild("door").gameObject;
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	//for scoring and difficulty multiplier
	public int currentFloor = 1;
	public GameObject currentRoom;
	public bool isExitPresent = false;

	private GameObject divergentRoom;
	private GameObject entryRoom;
	private GameObject exitRoom;
	private GameObject fountainRoom;
	private GameObject trapRoom;
	private GameObject smallTerminalRoom;
	private GameObject largeTerminalRoom;

	private float roomSpawnEnergy;

	public float currentX = 0f;
	private float currentY = 0f;
	
	void Awake() {
		// preload rooms
		divergentRoom = (GameObject)Resources.Load("prefabs/rooms/DivergentRoom", typeof(GameObject)); // 7/20
		entryRoom = (GameObject)Resources.Load("prefabs/rooms/EntryRoom", typeof(GameObject));
		exitRoom = (GameObject)Resources.Load("prefabs/rooms/ExitRoom", typeof(GameObject)); //4/20
		fountainRoom = (GameObject)Resources.Load("prefabs/rooms/FountainRoom", typeof(GameObject)); //1/20
		trapRoom = (GameObject)Resources.Load("prefabs/rooms/TrapRoom", typeof(GameObject)); //8/20
		//libraryRoom

		smallTerminalRoom = (GameObject)Resources.Load("prefabs/rooms/SmallTerminalRoom", typeof(GameObject));
		largeTerminalRoom = (GameObject)Resources.Load("prefabs/rooms/LargeTerminalRoom", typeof(GameObject));

		_instance = this;
	}
	
	void Start () {
		Debug.Log(entryRoom);
		roomSpawnEnergy = Random.Range((currentFloor * 3f), (currentFloor * 3.5f));
		currentRoom = GenerateEntryRoom();
	}

	public Room CurrentRoomController {
		get { return currentRoom.GetComponent<Room>(); }
	}

	public GameObject GenerateEntryRoom() {
		GameObject newRoom = (GameObject)Instantiate(entryRoom, new Vector2(currentX, currentY), Quaternion.identity);
		currentX += newRoom.GetComponent<Room>().area.x + 1;
		return newRoom;
	}

	public GameObject GenerateTerminalRoom() {
		//int p = Random.Range(0, 1);
		GameObject n = null;
		switch(Random.Range(0, 5)) {
			case 0:
				n = smallTerminalRoom;
				break;
			case 1:
				n = largeTerminalRoom;
				break;
			case 2:
				n = fountainRoom;
				break;
			case 3:
				n = fountainRoom;
				break;
			case 4:
				n = fountainRoom;
				break;
		}
		GameObject newRoom = (GameObject)Instantiate(n, new Vector2(currentX, currentY), Quaternion.identity);
		currentX += newRoom.GetComponent<Room>().area.x + 1;
		return newRoom;
	}

	public GameObject GenerateNewRoom(GameObject curr) {
		string newName = "";
		/*float p = Random.Range(0f, 1f);
		while() {

		}*/
		//TODO: make this crappy randomizer better later lol
		GameObject n = curr;
		if(roomSpawnEnergy > 0) {
			Debug.Log("spawn energy:" + roomSpawnEnergy);
			while(n.name == curr.name) {
				switch(Random.Range(0, 5)) {
				case 0:
					n = divergentRoom;
					newName = "DivergentRoom";
					break;
				case 1:
					n = trapRoom;
					newName = "TrapRoom";
					break;
				case 2:
					n = trapRoom;
					newName = "TrapRoom";
					break;
				case 3:
					n = divergentRoom;
					newName = "DivergentRoom";
					break;
				case 4:
					n = trapRoom;
					newName = "TrapRoom";
					break;
				}
			}

		} else {
			n = exitRoom;
			newName = "ExitRoom";
			isExitPresent = true;
		}

		roomSpawnEnergy -= n.GetComponent<Room>().roomCost;
		GameObject newRoom = (GameObject)Instantiate(n, new Vector2(currentX, currentY), Quaternion.identity);
		//newRoom.name = newName;

		currentX += newRoom.GetComponent<Room>().area.x + 1;

		return newRoom;
	}

	public void DisableGame() {
		//disable camera and player
		MainCameraController.Instance.gameObject.SetActive(false);
		PlayerController.Instance.gameObject.SetActive(false);
	}

	public void EnableGame() {
		//disable camera and player
		MainCameraController.Instance.gameObject.SetActive(true);
		PlayerController.Instance.gameObject.SetActive(true);
	}

	
	public void GameOver() {
		//disable camera and player
		MainCameraController.Instance.gameObject.SetActive(false);
		PlayerController.Instance.gameObject.SetActive(false);
		
		currentRoom = null;
		
		DestroyRooms();
		
		//reset level variables
		isExitPresent = false;
		currentX = 0;
		
		GenerateNewLevel();
		
		MainCameraController.Instance.gameObject.SetActive(true);
		
		PlayerController.Instance.Restart();
		
		//update camera
		MainCameraController.Instance.transform.position = new Vector3(-0.02448663f, 0.3689732f, MainCameraController.Instance.transform.position.z);
		MainCameraController.Instance.updateCamera();

		PlayerController.Instance.Restart();

		currentFloor = 1;
	}

	public void GenerateNewLevel() {
		//reset level variables
		isExitPresent = false;
		currentX = 0;
		roomSpawnEnergy = Random.Range((currentFloor * 3f), (currentFloor * 5f));
		currentRoom = null;
		//create new rooms
		currentRoom = GameController.Instance.GenerateEntryRoom();
	}
	
	public void DestroyRooms() {
		//destroy rooms
		foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room")) {
			Destroy(room);
		}
	}
	
	private static GameController _instance;

	public static GameController Instance {
		get { return _instance; }
	}


}

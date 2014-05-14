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
	
	// Use this for initialization
	void Start () {
		Debug.Log(entryRoom);
		currentRoom = GenerateEntryRoom();
		//Debug.Log(GameController.Instance.currentRoom.transform.FindChild("4-tile-piece"));
		//MainCameraController.Instance.gameObject.SetActive(false);
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
		switch(Random.Range(0, 2)) {
			case 0:
				n = smallTerminalRoom;
				break;
			case 1:
				n = largeTerminalRoom;
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
		GameObject n = curr;
		while(n.name == curr.name) {
			switch(Random.Range(0, 5)) {
			case 0:
				n = divergentRoom;
				newName = "DivergentRoom";
				break;
			case 1:
				n = exitRoom;
				isExitPresent = true;
				newName = "ExitRoom";
				Debug.Log("exit is present!");
				break;
			/*case 2:
				n = fountainRoom;
				newName = "FountainRoom";
				break;*/
			case 2:
				n = trapRoom;
				newName = "TrapRoom";
				break;
			
			case 3:
				n = trapRoom;
				newName = "TrapRoom";
				break;
			case 4:
				n = divergentRoom;
				newName = "DivergentRoom";
				break;
			case 5:
				n = divergentRoom;
				newName = "DivergentRoom";
				break;
			}
		
		}

		GameObject newRoom = (GameObject)Instantiate(n, new Vector2(currentX, currentY), Quaternion.identity);
		//newRoom.name = newName;

		currentX += newRoom.GetComponent<Room>().area.x + 1;

		return newRoom;
	}

	// Update is called once per frame
	void Update () {
	
	}

	private static GameController _instance;

	public static GameController Instance {
		get { return _instance; }
	}


}

using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {
	const float X_THRESHOLD = 1f;
	const float Y_THRESHOLD = 1f;

	//adjust depending on size of room
	public float MIN_X;
	//public float MAX_X = 4.4f;
	public float MAX_X;
	public float MIN_Y;
	public float MAX_Y;

	private static MainCameraController _instance;

	float initialX;
	float initialY;
	float newX;
	float newY;

	GameObject player;

	void Awake() {
		_instance = this;
	}

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		Debug.Log(player);
		initialX = player.transform.position.x;
		initialY = player.transform.position.y;

		updateCamera();

		Debug.Log(MIN_X);
	}

	public void updateCamera() {
		Room r = GameController.Instance.currentRoom.GetComponent<Room>();
		Debug.Log("area " + r.name);
		//min and max of camera are relative to the start and end points of each room
		//MIN_X = GameController.Instance.currentRoom.transform.position.x + r.area.x/4.7f;
		MIN_X = GameController.Instance.currentRoom.transform.position.x + 2.45f;
		MAX_X = GameController.Instance.currentRoom.transform.position.x + 8.79f;
		//MAX_X = GameController.Instance.currentRoom.transform.position.x + r.area.x/1.31f;
		//MIN_Y = GameController.Instance.currentRoom.transform.position.y + r.area.y/2.9f;
		MIN_Y = GameController.Instance.currentRoom.transform.position.y + 1.765f;
		MAX_Y = GameController.Instance.currentRoom.transform.position.y + 3.01f;
		//MAX_Y = GameController.Instance.currentRoom.transform.position.y + r.area.y/1.7f;

		transform.position = new Vector3(MIN_X, MIN_Y, -10);
		Debug.Log("new pos: " + transform.position);
	}

	void Update () {
		float deltaX = player.transform.position.x - initialX;
		float deltaY = player.transform.position.y - initialY;
		if(Mathf.Abs(deltaX) > X_THRESHOLD) {
			newX = Mathf.Lerp(transform.position.x, player.transform.position.x, 8f * Time.deltaTime);
		}
		if(Mathf.Abs(deltaY) > Y_THRESHOLD) {
			newY = Mathf.Lerp(transform.position.y, player.transform.position.y, 8f * Time.deltaTime);
		}

		//min and max should be relative to player's position and size of room
		newX = Mathf.Clamp(newX, MIN_X, MAX_X);
		newY = Mathf.Clamp(newY, MIN_Y, MAX_Y);

		transform.position = new Vector3(newX, newY, transform.position.z);
	}

	public static MainCameraController Instance {
		get { return _instance; }
	}
}
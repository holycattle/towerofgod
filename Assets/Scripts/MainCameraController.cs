using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {
	const float X_THRESHOLD = 1f;
	const float Y_THRESHOLD = 1f;

	const float MIN_X = 2.9f;
	const float MAX_X = 10f;

	const float MIN_Y = 1.75f;
	const float MAX_Y = 10f;

	float initialX;
	float initialY;
	float newX;
	float newY;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		Debug.Log(player);
		initialX = player.transform.position.x;
		initialY = player.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaX = player.transform.position.x - initialX;
		float deltaY = player.transform.position.y - initialY;
		Debug.Log(deltaX);
		if(Mathf.Abs(deltaX) > X_THRESHOLD) {
			Debug.Log("should be moving x");
			newX = Mathf.Lerp(transform.position.x, player.transform.position.x, 8f * Time.deltaTime);
		}
		if(Mathf.Abs(deltaY) > Y_THRESHOLD) {
			Debug.Log("should be moving x");
			newY = Mathf.Lerp(transform.position.y, player.transform.position.y, 8f * Time.deltaTime);
		}

		newX = Mathf.Clamp(newX, MIN_X, MAX_X);
		newY = Mathf.Clamp(newY, MIN_Y, MAX_Y);

		transform.position = new Vector3(newX, newY, transform.position.z);
	}
}

//2.399814
//1.758484
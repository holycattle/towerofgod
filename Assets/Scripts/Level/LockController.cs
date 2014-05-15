using UnityEngine;
using System.Collections;

public class LockController : MonoBehaviour {
	Room room;
	// Use this for initialization
	void Start () {
		room = transform.GetComponent<Room>();
	}
	
	// Update is called once per frame
	void Update () {
		//while enemies exist, keep doors in the level disabled
		room.Lock(room.Cleared);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour {
	public static GameObject flyingMonster;
	public static GameObject plantMonster;
	public static GameObject skullMonster;

	private IOrderedEnumerable<System.Collections.Generic.KeyValuePair<string,Enemy>> sortedDict;

	private Vector3 spawnPoint;

	Room room;

	public static Dictionary<string, Enemy> enemyDict;

	//total weight of all monsters
	public float totalWeight = 0f;

	// Use this for initialization

	void Awake() {
		room = GetComponent<Room>();
	}

	void Start () {


		flyingMonster = (GameObject)Resources.Load("prefabs/enemies/FlyingMonster", typeof(GameObject));
		plantMonster = (GameObject)Resources.Load("prefabs/enemies/PlantMonster", typeof(GameObject));
		skullMonster = (GameObject)Resources.Load("prefabs/enemies/SkullMonster", typeof(GameObject));

		enemyDict = new Dictionary<string, Enemy>();
		enemyDict.Add("FlyingMonster", flyingMonster.GetComponent<Enemy>());
		enemyDict.Add("SkullMonster", skullMonster.GetComponent<Enemy>());
		enemyDict.Add("PlantMonster", plantMonster.GetComponent<Enemy>());

		sortedDict = from entry in enemyDict orderby entry.Value.spawnCost ascending select entry;
	}

	GameObject GetRandomEnemy() {
		float lowerBound = 0f;
		float upperBound = 0f;
		float p = Random.Range(0f, 1f);

		if(totalWeight == 0f) {
			foreach(KeyValuePair<string, Enemy> pair in sortedDict) {
				totalWeight += pair.Value.spawnCost;
			}
		}

		if(room.spawnEnergy > 0) {
			//GameObject newEnemy = (GameObject)Instantiate(
			foreach(KeyValuePair<string, Enemy> pair in sortedDict) {
				upperBound += pair.Value.spawnCost/totalWeight;
				Debug.Log("lowerbound: " + lowerBound);
				Debug.Log("upperbound: " + upperBound);
				if(p >= lowerBound && p <= upperBound) {
					room.spawnEnergy -= pair.Value.spawnCost * (GameController.Instance.currentFloor/2f);
					return pair.Value.gameObject;
				} else {
					lowerBound += upperBound;
				}
			}
		}
		return null;
	}

	// Update is called once per frame
	void Update () {
		if(room.spawnEnergy > 0) {
			float x = 0f;
			float y = 0f;
			GameObject g = GetRandomEnemy();
			if(g != null) {
				if(g.GetComponent<Enemy>().type == "Flying") {
					x = Random.Range(room.transform.position.x + room.area.x/5, room.transform.position.x + room.area.x/2 + room.area.x/3);
					y = Random.Range(room.transform.position.y + room.area.y/4, room.transform.position.y + room.area.y/2);
					//spawnPoint = new Vector3(, Random.Range(currentRoom.area.x/2, currentRoom.area.x/2 + currentRoom.area.x/3), 0f);
				} else {
					x = Random.Range(room.transform.position.x + room.area.x/4, room.transform.position.x + room.area.x/2 + GameController.Instance.CurrentRoomController.area.x/3);
					y = 0.39f;
				}
				GameObject e = (GameObject)Instantiate(g, new Vector3(x, y, 0), Quaternion.identity);
				Debug.Log(e);
				e.transform.parent = room.transform;
			}
		}
	}
}

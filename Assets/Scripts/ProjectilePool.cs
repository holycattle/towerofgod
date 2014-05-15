using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour {
	//spell limit
	private const int SPELL_POOL_COUNT = 7;
	private const int ENEMY_PROJECTILE_COUNT = 50;
	//assumes that there are only two types of projectile for now; implement a more generalized ObjectPool later
	public GameObject[] spellPool;
	public GameObject[] enemyProjectilePool;

	private int enemyProjectileCounter = 0;

	private static ProjectilePool _instance;

	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		GameObject lightning = (GameObject)Resources.Load("prefabs/projectiles/lightning_spell", typeof(GameObject));
		GameObject projectilePool = GameObject.Find("ProjectilePool");
		spellPool = new GameObject[SPELL_POOL_COUNT];
		for(int i = 0; i < SPELL_POOL_COUNT; i++) {
			spellPool[i] = (GameObject)Instantiate(lightning, new Vector3(-100f, 0f, 0f), Quaternion.identity);
			spellPool[i].GetComponent<Spell>().enabled = false;
		}
		GameObject fireball = (GameObject)Resources.Load("prefabs/projectiles/fireball", typeof(GameObject));
		Debug.Log("fireball: "+fireball);
		enemyProjectilePool = new GameObject[ENEMY_PROJECTILE_COUNT];
		for(int i = 0; i < ENEMY_PROJECTILE_COUNT; i++) {
			enemyProjectilePool[i] = (GameObject)Instantiate(fireball, new Vector3(-100f, 0f, 0f), Quaternion.identity);
			enemyProjectilePool[i].transform.parent = projectilePool.transform;
			enemyProjectilePool[i].GetComponent<Spell>().enabled = false;
		}
	}

	public GameObject GetFireball(Transform parent, float direction) {
		int count = ENEMY_PROJECTILE_COUNT - GameObject.Find("ProjectilePool").transform.childCount;

		//Debug.Log("count: "+count);
		//Debug.Break();
		GameObject s;
		s = enemyProjectilePool[count];

		//set to parent's position
		Debug.Log("c: "+s.transform.position);
		s.transform.parent = null;
		s.transform.position = new Vector3(parent.position.x, parent.position.y, 0f);
		s.GetComponent<Spell>().direction = (int)direction;
		Debug.Log("c: "+s.transform.position);
		s.GetComponent<Spell>().enabled = true;
		return s;

	}

	public void Recycle(GameObject g) {
		g.GetComponent<Spell>().enabled = false;
		g.transform.position = new Vector3(100f, 0f, 0f);
		g.transform.parent = GameObject.Find("ProjectilePool").transform;
		Debug.Log("recycling: "+g);
	}

	//turn this into a more generalized function later once more projectiles have been added
	public GameObject GetLightning(Transform parent, float direction) {
		Debug.Log("p: "+ parent);
		int count = 0;

		foreach(Spell sp in GameObject.FindObjectsOfType<Spell>()) {
			if(sp.enabled) {
				count++;
			}
		}

		GameObject s;
		if(count == 0 || count == 7) {
			s = spellPool[0];
		} else {
			s = spellPool[count - 1];
		}
		//set to parent's position
		Debug.Log("c: "+s.transform.position);
		s.transform.position = new Vector3(parent.position.x, parent.position.y, 0f);
		Debug.Log("c: "+s.transform.position);
		s.GetComponent<Spell>().enabled = true;
		return s;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static ProjectilePool Instance {
		get { return _instance; }
	}
}

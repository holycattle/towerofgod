using UnityEngine;
using System.Collections;

public class Flying : MonoBehaviour {
	public const float FLIP_COOLDOWN = 2.5f;
	public const float AUTO_MOVE_RANGE = 1f;
	//melee?
	//TODO: set these to MELEE, RANGED, etc. constants later
	public const float ATTACK_RANGE = 3f;
	public const float SIGHT_RANGE = 3f;
	
	GameObject enemyObject;
	
	Transform target;
	bool facingRight = true;
	public int direction = 1;
	float initialX;

	public float moveSpeed;
	
	float flipCooldown = 0f;
	
	// Use this for initialization
	void Start () {
		//set this to root's transform
		enemyObject = this.gameObject.transform.gameObject;
		Debug.Log("eo: "+ enemyObject);
		target = null;
		initialX = enemyObject.transform.position.x;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Default behavior:
		//periodically look left and right
		//check if enemy is within range
		//move towards enemy

		if(Mathf.Abs(PlayerController.Instance.transform.position.x - transform.position.x) <= SIGHT_RANGE) {
			target = PlayerController.Instance.transform;
		} else {
			target = null;
		}

		if(target == null) {
			if(Mathf.Abs(initialX - enemyObject.transform.position.x) < AUTO_MOVE_RANGE) {
				enemyObject.transform.Translate(new Vector2(Time.fixedDeltaTime * moveSpeed * direction, 0));
			} else {
				initialX = enemyObject.transform.position.x;
				Flip();
			}
		} else {
			if(enemyObject.transform.position.x > target.position.x && facingRight) {
				Flip();
			} else if(enemyObject.transform.position.x < target.position.x && !facingRight) {
				Flip();
			}

			//if within range, attack
			if(Mathf.Abs(target.position.x - enemyObject.transform.position.x) <= ATTACK_RANGE) {
				enemyObject.GetComponent<Enemy>().Attack();
				
			}
		}
		
	}
	
	/*void OnTriggerStay2D(Collider2D c) {
		Debug.Log("player on sight! "+c.gameObject);
		if(c.gameObject.transform.parent.name == "Player") {
			Debug.Log("player on sight! "+c.gameObject);
			target = c.gameObject.transform.parent;
			Debug.Log("target: "+target.name);

			if(enemyObject.transform.position.x > target.position.x && facingRight) {
				Flip();
			} else if(enemyObject.transform.position.x < target.position.x && !facingRight) {
				Flip();
			}
		}
		
	}
	
	void OnTriggerExit2D(Collider2D c) {
		Debug.Log("left! " + c.gameObject);
		if(c.gameObject.transform.parent.name == "Player") {
			target = null;
		}
	}*/
	
	void Flip() {
		Vector3 newScale = enemyObject.transform.localScale;
		newScale.x *= -1;
		facingRight = !facingRight;
		enemyObject.transform.localScale = newScale;
		direction *= -1;
	}

}

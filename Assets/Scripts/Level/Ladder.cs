using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
	private float climbingSpeed = 1.75f;
	private bool withinLadder = false;
	private GameObject player;
	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate() {
		if(withinLadder) {
			float direction = Input.GetAxis("Vertical");
			if(Mathf.Abs(direction) > 0.1f) {
				player.rigidbody2D.gravityScale = 0;
				Debug.Log("Should be climbing");	
				//player.transform.Translate(new Vector2(0, Time.fixedDeltaTime * climbingSpeed * direction));
				player.transform.rigidbody2D.velocity = new Vector2(player.transform.rigidbody2D.velocity.x, climbingSpeed * direction);
			}
			if(Input.GetButtonDown("Jump")) {
				player.rigidbody2D.gravityScale = 9.81f;
			}
		}

	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.name == "Player") {
			withinLadder = true;
			player = other.gameObject;
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
			Debug.Log(Physics2D.GetIgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform")));
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.name == "Player") {
			withinLadder = false;
			other.gameObject.rigidbody2D.gravityScale = 9.81f;
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
			Debug.Log(Physics2D.GetIgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform")));
		}
	}
}

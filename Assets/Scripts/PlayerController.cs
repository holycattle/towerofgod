/*
1. flip animation based on left or right key
2. set velocity
*/
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private const float MAX_SPEED = 1000f;
	private const float JUMPING_FORCE = 1750f;
	private const float MOVE_FORCE = 100f;
	private bool facingRight;
	private bool jumping;
	
	Animator animator;
	// Use this for initialization
	void Start() {
		animator = this.GetComponent<Animator>();
		facingRight = true;
		jumping = false;
	}

	void Update() {
		jumping = Input.GetButtonDown("Jump");
	}

	// Update is called once per frame
	void FixedUpdate() {
		float direction = Input.GetAxis("Horizontal");
		//Debug.Log(direction);
		animator.SetFloat("Speed", Mathf.Abs(direction));

		//if velocity hasn't maxed out yet, keep adding force
		if(Mathf.Abs(direction) > 0) {
			rigidbody2D.AddForce(Vector2.right * direction * MOVE_FORCE);
		}
		//if velocity exceeds max, snap velocity to max speed
		if(rigidbody2D.velocity.x > MAX_SPEED) {
			rigidbody2D.velocity = new Vector2(Mathf.Sign(direction) * MAX_SPEED, rigidbody2D.velocity.y);
		}

		if(jumping) {
			//TODO: check if player is on the ground
			animator.SetTrigger("Jump");
			Debug.Log("Jump!");

			rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x, JUMPING_FORCE));
			//rigidbody2D.velocity.Set(rigidbody2D.velocity.x, 100f);
			Debug.Log(rigidbody2D.velocity);
			jumping = false;
		}

		if (direction > 0 && !facingRight) {
			Flip();
		} else if (direction < 0 && facingRight) {
			Flip();
		}

		//transform.Translate(new Vector2(Time.fixedDeltaTime * MAX_SPEED * direction, 0));
	}
	
	void Flip() {
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
		facingRight = !facingRight;
	}
}
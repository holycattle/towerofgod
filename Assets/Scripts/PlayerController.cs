/*
1. flip animation based on left or right key
2. set velocity
*/
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private const float MAX_SPEED = 0.75f;
	private const float JUMPING_FORCE = 1500f;
	private const float MOVE_FORCE = 0.25f;
	private bool facingRight;
	private bool jumping;
	
	Animator animator;
	// Use this for initialization
	void Start() {
		animator = this.GetComponent<Animator>();
		facingRight = true;
		jumping = false;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		float direction = Input.GetAxis("Horizontal");
		Debug.Log(direction);
		animator.SetFloat("Speed", Mathf.Abs(direction));

		if(Input.GetButtonDown("Jump") && !jumping) {
			//TODO: check if player is on the ground
			jumping = true;
			animator.SetTrigger("Jump");
			rigidbody2D.AddForce(new Vector2(0f, JUMPING_FORCE));
			jumping = false;
		}

		if (direction > 0 && !facingRight) {
			Flip();
		} else if (direction < 0 && facingRight) {
			Flip();
		}
		transform.Translate(new Vector3(Time.fixedDeltaTime * MAX_SPEED * direction, 0, 0));
	}
	
	void Flip() {
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
		facingRight = !facingRight;
	}
}
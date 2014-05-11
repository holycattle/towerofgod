/*
1. flip animation based on left or right key
2. set velocity
*/
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private const float MAX_SPEED = 0.75f;
	private const float JUMPING_FORCE = 20f;
	private bool facingRight;
	
	Animator animator;
	// Use this for initialization
	void Start() {
		animator = this.GetComponent<Animator>();
		facingRight = true;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		float d = Input.GetAxis("Horizontal");
		/*if(Input.GetButtonDown("Jump")) {
			//TODO: check if player is on the ground
			rigidbody2D.AddForce(new Vector2(0f, JUMPING_FORCE));
			Debug.Log(rigidbody2D.velocity.y);
		}*/

		animator.SetFloat("Speed", Mathf.Abs(d));
		//animator.SetFloat("verticalVelocity", rigidbody2D.velocity.y);
		
		if (d > 0 && !facingRight) {
			Flip();
		} else if (d < 0 && facingRight) {
			Flip();
		}
		transform.Translate(new Vector3(Time.fixedDeltaTime * MAX_SPEED * d, 0, 0));
	}
	
	void Flip() {
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
		facingRight = !facingRight;
	}
}
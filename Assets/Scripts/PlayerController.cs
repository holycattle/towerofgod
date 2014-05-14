/*
1. flip animation based on left or right key
2. set velocity
*/
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private const float MAX_SPEED = 2f;
	private const float JUMPING_FORCE = 500f;
	private const float MAX_JUMPING_FORCE = 20000f;
	private const float MOVE_FORCE = 100f;
	private bool facingRight;
	private bool jumping;
	private bool attacking;

	public float health;
	public float attack;

	private static PlayerController _instance;

	void Awake() {
		_instance = this;
	}

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

	void FixedUpdate() {
		float direction = Input.GetAxis("Horizontal");
		//Debug.Log(direction);
		animator.SetFloat("Speed", Mathf.Abs(direction));

		//if velocity hasn't maxed out yet, keep adding force
		if(Mathf.Abs(direction) > 0) {
			//rigidbody2D.velocity = new Vector2(Mathf.Sign(direction) * MAX_SPEED, rigidbody2D.velocity.y);
			transform.Translate(new Vector2(Time.fixedDeltaTime * 1.2f * direction, 0));
			Debug.Log("should be moving: " + Mathf.Sign(direction) * MAX_SPEED * Time.deltaTime);
		}

		if(jumping) {
			//TODO: check if player is on the ground
			animator.SetTrigger("Jump");
			Debug.Log("Jump!");

			//rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x, JUMPING_FORCE));
			rigidbody2D.velocity = new Vector2(direction * 12f, 35f);
			Debug.Log(rigidbody2D.velocity);
			/*if(rigidbody2D.velocity.y >= MAX_JUMPING_FORCE) {
				jumping = false;
			}*/
		}

		if(rigidbody2D.velocity.y > 0) {
			Debug.Log("should be ignoring platforms");
			Debug.Log(LayerMask.NameToLayer("Platform"));
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
		} else {
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
		}

		if(direction > 0 && !facingRight) {
			Flip();
		} else if(direction < 0 && facingRight) {
			Flip();
		}

		//transform.Translate(new Vector2(Time.fixedDeltaTime * 0.75f * direction, 0));
	}
	
	void Flip() {
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
		facingRight = !facingRight;
	}

	public static PlayerController Instance {
		get { return _instance; }
	}
}
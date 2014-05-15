/*
1. flip animation based on left or right key
2. set velocity
*/
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private const float COOLDOWN = 0.2f;

	private const float MAX_SPEED = 2f;
	private const float JUMPING_FORCE = 500f;
	private const float MAX_JUMPING_FORCE = 20000f;
	private const float MOVE_FORCE = 100f;
	public bool facingRight;
	private bool jumping;
	private bool attacking;

	public float health;
	public float attack;

	public bool grounded = true;
	public int timesJumped = 0;

	private GameObject lightningPrefab;

	private float attackCooldown = 0f;

	private static PlayerController _instance;

	private GameObject damageCounter;

	void Awake() {
		_instance = this;
	}

	Animator animator;
	// Use this for initialization
	void Start() {
		animator = this.GetComponent<Animator>();
		facingRight = true;
		jumping = false;
		attacking = false;

		lightningPrefab = (GameObject)Resources.Load("prefabs/projectiles/lightning_spell", typeof(GameObject));
		//TODO: load this only once!
		damageCounter = (GameObject)Resources.Load("prefabs/UI/DamageCounter", typeof(GameObject));

		health = 20f;
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
			//Debug.Log("should be moving: " + Mathf.Sign(direction) * MAX_SPEED * Time.deltaTime);
		}

		if(jumping && (timesJumped < 2)) {
			//TODO: check if player is on the ground
			timesJumped++;
			animator.SetTrigger("Jump");
			//Debug.Log("Jump!");

			//rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x, JUMPING_FORCE));
			rigidbody2D.velocity = new Vector2(direction * 12f, 35f);
			Debug.Log(rigidbody2D.velocity);
			/*if(rigidbody2D.velocity.y >= MAX_JUMPING_FORCE) {
				jumping = false;
			}*/
			jumping = false;
		}

		if(Input.GetButtonDown("Fire1") && attackCooldown <= 0) {
			animator.SetTrigger("Attack");
			Debug.Log("attack!");
			attackCooldown = COOLDOWN;
			//release projectile
			//ProjectilePool.Instance.GetLightning(this.transform.FindChild("Projectiles"), facingRight? 1:-1);
			Transform tmp = transform.FindChild("Projectiles");
			GameObject g = (GameObject)Instantiate(lightningPrefab, new Vector3(tmp.position.x, tmp.position.y, 0), Quaternion.identity);
			g.GetComponent<Spell>().damage = attack;
			g.GetComponent<Spell>().direction = facingRight? 1:-1;
		}

		//attack cooldown
		if(attackCooldown > 0) {
			attackCooldown -= Time.deltaTime;
		}

		if(attackCooldown < 0) {
			attackCooldown = 0f;
		}

		if(direction > 0 && !facingRight) {
			Flip();
		} else if(direction < 0 && facingRight) {
			Flip();
		}

		//death
		if(health <= 0) {
			//die
			GameController.Instance.GameOver();
		}

		//transform.Translate(new Vector2(Time.fixedDeltaTime * 0.75f * direction, 0));
	}
	
	void Flip() {
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
		facingRight = !facingRight;
	}

	public void Restart() {
		health = 20f;
		//clear everything gained

		//reset player position
		gameObject.SetActive(true);
		transform.position = new Vector3(-0.02448663f, 0.3689732f, transform.position.z);
	}

	public static PlayerController Instance {
		get { return _instance; }
	}

	public void TakeDamage(float damage) {
		health -= damage;
		//make a pool for this later on too
		GameObject damageCounterUI = (GameObject)Instantiate(damageCounter, Camera.main.WorldToViewportPoint(gameObject.transform.position), Quaternion.identity);
		damageCounterUI.GetComponent<DamageCounter>().isDamage = true;
		damageCounterUI.guiText.text = ""+ Mathf.Ceil(damage);
		Debug.Log(health);
	}

}
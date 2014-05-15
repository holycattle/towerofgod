using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float health;
	public float damage;

	public int spawnCost;

	public float cooldown = 1f;
	public float cooldownTimer = 0f;

	//turn this into an enum later
	public string type;

	private GameObject fireballPrefab;

	private Animator animator;

	//TODO: refactor this to repeat repetition from PlayerControl
	private GameObject damageCounter;

	// Use this for initialization
	void Start () {
		damageCounter = (GameObject)Resources.Load("prefabs/UI/DamageCounter", typeof(GameObject));
		animator = this.GetComponent<Animator>();
		fireballPrefab = (GameObject)Resources.Load("prefabs/projectiles/fireball");
	}

	void OnCollisionEnter2D(Collision2D collision) {
		//later, check if object has a TakeDamage function
		if(collision.gameObject.tag == "Player") {
			PlayerController.Instance.TakeDamage(damage/2f);
		}
	}

	void Update() {
		if(health <= 0f) {
			Destroy(gameObject);
		}
	}

	public virtual void Attack() {
		//animator.SetTrigger("Attack");

		//check if long range
		if(cooldownTimer <= 0) {
			//if melee, raycast and damage if it hits anything within a short range
			cooldownTimer = cooldown;
			Transform tmp = transform.FindChild("Projectiles");
			if(gameObject.tag == "Melee") {
				//.25f == melee range
				RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 0.25f);
				if(hit.collider.gameObject.name == "Player") {

					Debug.Log("enemy attacked player!");
					Debug.Break();
					PlayerController.Instance.TakeDamage(damage);
				}
			} else if(gameObject.tag == "Ranged") {
				//fire projectile
				//HACK: since the flying guy, for now, is the only one with ranged capabilities, get Flying component
				//TODO: make a superclass for Land and Flying and other types of AI to handle direction
				Debug.Log("fire!!!!: "+transform.FindChild("Projectiles"));

				GameObject g = (GameObject)Instantiate(fireballPrefab, new Vector3(tmp.position.x, tmp.position.y, 0), Quaternion.identity);
				g.GetComponent<Spell>().direction = gameObject.GetComponent<Flying>().direction;
				g.GetComponent<Spell>().damage = damage;
				//ProjectilePool.Instance.GetFireball(transform.FindChild("Projectiles"), gameObject.GetComponent<Flying>().direction);
				//Debug.Break();
			}
		} else {
			cooldownTimer -= Time.deltaTime;
		}
	}

	public virtual void TakeDamage(float d) {
		health -= d;
		GameObject damageCounterUI = (GameObject)Instantiate(damageCounter, Camera.main.WorldToViewportPoint(gameObject.transform.position), Quaternion.identity);
		damageCounterUI.GetComponent<DamageCounter>().isDamage = true;
		damageCounterUI.guiText.text = ""+ Mathf.Ceil(d);
		Debug.Log("enemy h: "+health);
	}
}

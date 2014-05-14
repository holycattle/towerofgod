using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float health;
	public float damage;

	public int spawnCost;

	//turn this into an enum later
	public string type;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}

	public virtual void Attack() {
		animator.SetTrigger("Attack");
	}
}

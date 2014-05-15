using UnityEngine;
using System.Collections;

//TODO: create a parent class called Projectile that handles normal projectile stuff

public class Spell : MonoBehaviour {
	public float damage;
	public int direction = 1;

	// Use this for initialization
	void Start () {


	}

	void FixedUpdate() {
		transform.Translate(new Vector2(Time.fixedDeltaTime * 2f * direction, 0));
		float x = MainCameraController.Instance.camera.WorldToScreenPoint(this.transform.position).x;
		if(x > MainCameraController.Instance.transform.position.x + MainCameraController.Instance.camera.pixelWidth || x < MainCameraController.Instance.transform.position.x) {
			//HACK!!
			Destroy(this.gameObject);
			/*if(!(gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))) {
				Destroy(this.gameObject);
			} else {
				ProjectilePool.Instance.Recycle(this.gameObject);
			}*/
		}
	}

	void OnCollisionEnter2D(Collision2D c) {
		Debug.Log("layer: " + c.gameObject.layer);
		Debug.Log("layername: " +LayerMask.LayerToName(c.gameObject.layer));
		Enemy e = c.gameObject.GetComponent<Enemy>();
		if(e != null) {
			Debug.Log("hit!");
			Destroy(this.gameObject);
			e.TakeDamage(damage);
			/*if(!(gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))) {
				Destroy(this.gameObject);
			} else {
				ProjectilePool.Instance.Recycle(this.gameObject);
			}*/
		} else if(c.gameObject.layer == LayerMask.NameToLayer("Platform")) {
			Destroy(this.gameObject);
			/*//HACK!!
			if(!(gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))) {
				Destroy(this.gameObject);
			} else {
				ProjectilePool.Instance.Recycle(this.gameObject);
			}*/
		} else if(c.gameObject.layer == LayerMask.NameToLayer("Player")) {
			PlayerController.Instance.TakeDamage(damage);
			Debug.Log ("asdf");
			Destroy(this.gameObject);
			/*if(!(gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))) {
				Destroy(this.gameObject);
			} else {
				Debug.Log("recycle");
				ProjectilePool.Instance.Recycle(this.gameObject);
			}*/
		}

		//this.enabled = false;
		//gameObject.SetActive(false);
		//transform.position = new Vector3(100f, 0f, 0f);

	}
}

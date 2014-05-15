using UnityEngine;
using System.Collections;

public class Fountain : MonoBehaviour {

	//lol refactor this idiot
	private GameObject healCounter;

	void Start() {
		healCounter = (GameObject)Resources.Load("prefabs/UI/DamageCounter", typeof(GameObject));
	}

	void OnTriggerStay2D(Collider2D other) {
		Debug.Log("inside fountain! "+other.gameObject.name);
		if(other.gameObject.name == "Player") {
			Debug.Log("should heal");
			GameObject damageCounterUI = (GameObject)Instantiate(healCounter, Camera.main.WorldToViewportPoint(gameObject.transform.position), Quaternion.identity);
			damageCounterUI.guiText.text = ""+ Mathf.Ceil(20f - PlayerController.Instance.health);
			damageCounterUI.GetComponent<DamageCounter>().isDamage = false;
			PlayerController.Instance.health = 20f;
		}
	}
}

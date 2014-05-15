using UnityEngine;
using System.Collections;

public class GroundChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//check if on the ground
		RaycastHit2D raycasted = Physics2D.Raycast(transform.position, new Vector2(transform.up.x, transform.up.y*-1), 0.2f);
		if(raycasted.collider != null) {
			if(raycasted.collider.gameObject.layer == LayerMask.NameToLayer("Platform")) {
				PlayerController.Instance.grounded = true;
				PlayerController.Instance.timesJumped = 0;
			}
		}
	}
}

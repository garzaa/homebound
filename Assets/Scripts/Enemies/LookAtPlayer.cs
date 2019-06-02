using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

	bool facingRight;
	Transform target;
	Entity e;

	void Start() {
		target = PlayerSwitcher.currentPlayer.transform;
		e = GetComponent<Entity>();
	}

	void Update() {
		if (e != null) {	
			facingRight = e.IsFacingRight();
		}
		if (facingRight) {
			if (target.transform.position.x < this.transform.position.x) {
				Flip();
			}
		} else {
			if (target.transform.position.x > this.transform.position.x) {
				Flip();
			}
		}
	}

	void Flip() {
		if (e != null) {
			e.Flip();
		} else {
			this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
			facingRight = !facingRight;
		}
	}
}

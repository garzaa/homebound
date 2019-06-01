using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSensor : Sensor {

	Rigidbody2D rb2d;

	new void Start() {
		base.Start();
		rb2d = e.GetComponent<Rigidbody2D>();
	}

	void Update () {
		animator.SetFloat("XSpeed", Mathf.Abs(rb2d.velocity.x));
		animator.SetFloat("YSpeed", rb2d.velocity.y);
	}
}

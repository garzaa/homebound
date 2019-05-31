using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

	public Animator animator;
	public Entity e;
	protected GameObject player;
	protected CombatController pc;

	protected void Start() {
		if (e == null) {
			e = GetComponent<Entity>();
		}
		if (animator == null) {
			animator = GetComponent<Animator>();
		}
	}
}

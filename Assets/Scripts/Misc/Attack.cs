using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    
    public AttackType type;
    public int damage = 1;
    public float hitstopLength = 0.1f;
    public bool hasKnockback;
    public Vector2 knockback;
    public GameObject hitmarker;

    public int CalculateDamage() {
        return damage;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Hurtbox hurtbox = other.GetComponent<Hurtbox>();
        if (hurtbox == null || !hurtbox.HitBy(this.type)) {
            return;
        }
        // now we're in on hit territory
        if (hitmarker != null) {
            //TODO: calculate the right position (average point?)
            Instantiate(hitmarker, this.transform);
        }
        Hitstop.Run(hitstopLength);
        hurtbox.OnHit(this);
    }

}

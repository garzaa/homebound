using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    
    public AttackType type;
    public int damage = 1;
    public float hitstopLength = 0.1f;
    public bool hasKnockback;
    [Range(0, 1f)]
	public float cameraShakeIntensity = 0.1f;
	[Range(0, 2f)]
	public float cameraShakeTime = 0.1f;
    public Vector2 knockback;
    public GameObject hitmarker;

    Entity entityParent;

    void Start() {
        entityParent = GetComponentInParent<Entity>();
    }

    public int CalculateDamage() {
        return damage;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Hurtbox hurtbox = other.GetComponent<Hurtbox>();
        if (hurtbox == null || !hurtbox.HitBy(this.type)) {
            return;
        }
        OnAttackLand(hurtbox);
        hurtbox.OnHit(this);
    }

    void OnAttackLand(Hurtbox hurtbox) {
        // now we're in on hit territory
        if (hitmarker != null) {
            //TODO: calculate the right position (average point?)
            print("instantiating hitmarker");
            Instantiate(hitmarker, entityParent.transform);
        }
        if (cameraShakeTime > 0f) {
			CameraShaker.Shake(cameraShakeIntensity, cameraShakeTime);
		}
        Hitstop.Run(hitstopLength);
    }

    public Vector2 GetKnockback(Hurtbox hurtbox) {
        float scalar = (transform.position.x < hurtbox.transform.position.x ? 1 : -1);
        return new Vector2(knockback.x * scalar, knockback.y);
    }

}

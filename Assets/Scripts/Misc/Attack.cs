using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    
    public AttackType type;
    public int damageLowBound = 1;
    public int damageHighBound = 3;
    public float critChance = 0.1f;
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

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        Hurtbox hurtbox = other.GetComponent<Hurtbox>();
        if (hurtbox == null || !hurtbox.HitBy(this.type)) {
            return;
        }
        OnAttackLand(hurtbox);
        hurtbox.OnHit(this);
    }

    public DamageOutput CalculateDamage() {
        int initialDamage = Random.Range(damageLowBound, damageHighBound+1);
        if (Random.Range(0f, 1f) < critChance) {
            initialDamage *= 2;
            return new DamageOutput(initialDamage, true);
        }
        return new DamageOutput(initialDamage);
    }

    protected virtual void OnAttackLand(Hurtbox hurtbox) {
        if (hitmarker != null) {
            Transform t = this.transform;
            if (entityParent != null) {
                t = entityParent.transform;
            }
            InstantiateHitmarker(t, hurtbox);
        }
        if (cameraShakeTime > 0f) {
			CameraShaker.Shake(cameraShakeIntensity, cameraShakeTime);
		}
        Hitstop.Run(hitstopLength);
    }

    public Vector2 GetKnockback(Hurtbox hurtbox) {
        float xPos = entityParent != null ? entityParent.transform.position.x : this.transform.position.x;
        float scalar = (xPos < hurtbox.transform.position.x ? 1 : -1);
        return new Vector2(knockback.x * scalar, knockback.y);
    }

    protected virtual void InstantiateHitmarker(Transform t, Hurtbox hurtbox) {
        Instantiate(hitmarker, t).transform.position = hurtbox.transform.position;
    }

}

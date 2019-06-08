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
    public bool rotateHitmarker = true;
    public bool hitsProjectiles = false;
    public Vector2 projectileKnockback;

    Entity entityParent;

    void Start() {
        entityParent = GetComponentInParent<Entity>();
    }

    virtual protected void OnTriggerEnter2D(Collider2D other) {
        Hurtbox hurtbox = other.GetComponent<Hurtbox>();
        if (hurtbox == null || !hurtbox.HitBy(this.type)) {
            return;
        }
        hurtbox.OnHit(this);
        OnAttackLand(hurtbox);
    }

    virtual protected void OnAttackLand(Hurtbox hurtbox) {
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

    virtual protected void InstantiateHitmarker(Transform t, Hurtbox hurtbox) {
        GameObject h = Instantiate(hitmarker, t);
        h.transform.position = hurtbox.transform.position;
        if (rotateHitmarker) {
            h.transform.eulerAngles = new Vector3(
                0,
                0,
                Vector2.Angle(Vector2.right, knockback)
            );
        }
    }

    public Vector2 GetKnockback(Hurtbox hurtbox) {
        float xPos = entityParent != null ? entityParent.transform.position.x : this.transform.position.x;
        float scalar = (Behind(hurtbox.transform) ? 1 : -1);
        return new Vector2(knockback.x * scalar, knockback.y);
    }

    public DamageOutput CalculateDamage() {
        int initialDamage = Random.Range(damageLowBound, damageHighBound+1);
        if (Random.Range(0f, 1f) < critChance) {
            initialDamage *= 2;
            return new DamageOutput(initialDamage, true);
        }
        return new DamageOutput(initialDamage);
    }

    public bool Behind(Transform other) {
        Transform t = entityParent.transform ?? this.transform;
        return t.position.x < other.position.x;
    }

}

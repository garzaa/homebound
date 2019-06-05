using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Attack {

    public GameObject impactHitmarker;

    Rigidbody2D rb2d;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    override protected void OnAttackLand(Hurtbox hurtbox) {
        base.OnAttackLand(hurtbox);
        Destroy(this.gameObject);
    }

    override protected void InstantiateHitmarker(Transform t, Hurtbox hurtbox) {
        // casting instantiated objects is weird...don't worry about it
        // instantiate as a child of the projectile to preserve orientation/scale
        GameObject h = Instantiate(hitmarker, t.position, Quaternion.identity, this.transform).gameObject;
        h.transform.parent = null;
        Destroy(this.gameObject);
    }

    override protected void OnTriggerEnter2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == Layers.Ground) {
            Destroy(this.gameObject);
        } else if (other.GetComponent<Attack>() != null) {
            if (other.GetComponent<Attack>().hitsProjectiles) {
                ReflectFrom(other.GetComponent<Attack>());
            }
        } else {
            base.OnTriggerEnter2D(other);
        }
    }

    void ReflectFrom(Attack otherAttack) {
        if (impactHitmarker != null) {
            GameObject h = Instantiate(impactHitmarker);
            h.transform.eulerAngles = new Vector3(
                0,
                0,
                Vector2.Angle(Vector2.right, rb2d.velocity)
            );
        }
        GetComponent<Rigidbody2D>().velocity = otherAttack.projectileKnockback;
    }

}
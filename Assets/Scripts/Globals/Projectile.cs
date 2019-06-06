using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Attack {

    public GameObject impactHitmarker;
    [SerializeField] float minimumDamageVelocity;
    [SerializeField] int maxBounces;
    Rigidbody2D rb2d;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    override protected void OnAttackLand(Hurtbox hurtbox) {
        base.OnAttackLand(hurtbox);
        Destroy(this.gameObject);
    }

    override protected void InstantiateHitmarker(Transform t, Hurtbox hurtbox) {
        GameObject h = Instantiate(hitmarker, t);
        print(h.name);
        h.transform.position = hurtbox.transform.position;
        h.transform.eulerAngles = new Vector3(
            0,
            0,
            Vector2.Angle(Vector2.right, this.rb2d.velocity.normalized)
        );
        print(h.transform.eulerAngles);
    }

    override protected void OnTriggerEnter2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == Layers.Ground) {
            TryToBounce();
            return;
        }
        
        if (other.GetComponent<Attack>() != null) {
            if (other.GetComponent<Attack>().hitsProjectiles) {
                ReflectFrom(other.GetComponent<Attack>());
                return;
            }
        }
        
        if (rb2d.velocity.magnitude > minimumDamageVelocity) {
            base.OnTriggerEnter2D(other);
        }
    }

    void ReflectFrom(Attack otherAttack) {
        Hitstop.Run(otherAttack.hitstopLength);
        if (impactHitmarker != null) {
            GameObject h = Instantiate(impactHitmarker);
            h.transform.eulerAngles = new Vector3(
                0,
                0,
                Vector2.Angle(Vector2.right, rb2d.velocity)
            );
        }
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(
            otherAttack.Behind(this.transform) ? otherAttack.projectileKnockback.x : -otherAttack.projectileKnockback.x,
            otherAttack.projectileKnockback.y
        );
    }

    void TryToBounce() {
        if (maxBounces <= 0) {
            Destroy(this.gameObject);
            return;
        }
        // let the physics engine take care of it 
        maxBounces--;
    }

}
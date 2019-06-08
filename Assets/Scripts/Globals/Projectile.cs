using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Attack {

    public GameObject impactHitmarker;
    [SerializeField] float minimumDamageVelocity;
    [SerializeField] int maxBounces;
    Rigidbody2D rb2d;
    TrailRenderer damageTrail;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        damageTrail = GetComponent<TrailRenderer>();
    }

    void Update() {
        damageTrail.enabled = AtDamageSpeed();
    }
    
    override protected void OnAttackLand(Hurtbox hurtbox) {
        base.OnAttackLand(hurtbox);
        Destroy(this.gameObject);
    }

    override protected void InstantiateHitmarker(Transform t, Hurtbox hurtbox) {
        GameObject h = Instantiate(hitmarker, t.position, Quaternion.identity, null);
        h.transform.position = hurtbox.transform.position;
        RotateHitmarker(h);
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
        
        if (AtDamageSpeed()) {
            base.OnTriggerEnter2D(other);
        }
    }

    void ReflectFrom(Attack otherAttack) {
        Hitstop.Run(otherAttack.hitstopLength);
        InstantiateImpactHitmarker();
        
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
        if (AtDamageSpeed()) {
            InstantiateImpactHitmarker();
        }
    }

    void InstantiateImpactHitmarker() {
        if (impactHitmarker == null) {
            return;
        }
        GameObject h = Instantiate(impactHitmarker, this.transform.position, Quaternion.identity, null);
        RotateHitmarker(h);
    }

    bool AtDamageSpeed() {
        return rb2d.velocity.magnitude > minimumDamageVelocity;
    }

    void RotateHitmarker(GameObject h) {
        h.transform.eulerAngles = new Vector3(
            0,
            0,
            Vector2.Angle(Vector2.right, this.rb2d.velocity.normalized)
        );
    }

}
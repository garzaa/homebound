using UnityEngine;

public class Projectile : Attack {
    
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
            return;
        }
        base.OnTriggerEnter2D(other);
    }

}
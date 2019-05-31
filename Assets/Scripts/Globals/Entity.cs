using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    public bool canFlip = true;
    public int hp;
	public int totalHP;
    public bool grounded;

    protected bool facingRight = true;
    protected Animator animator;

    protected virtual void Start() {
        animator = GetComponent<Animator>();
    }

    public virtual void Flip() {
        if (!canFlip) {
            return;
        }
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

    public int ForwardScalar() {
        return facingRight ? 1 : -1;
    }

    public Vector2 ForwardVector() {
        return new Vector2(
            ForwardScalar(),
            0
        );
    }

    public virtual void OnGroundHit() {
        animator.SetBool("Grounded", true);
        grounded = true;
    }

    public virtual void OnGroundLeave() {
        animator.SetBool("Grounded", false);
        grounded = false;
    }

    public virtual void Die() {
        Destroy();
    }

    public virtual void OnHit(Attack a) {
        DamageFor(a.CalculateDamage());
    }

    public virtual void DamageFor(int amount) {
        hp -= amount;
        if (hp <= 0) {
            Die();
        }
    }
}

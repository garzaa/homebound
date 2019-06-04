using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    public bool canFlip = true;
    public int hp;
	public int totalHP;
    public bool grounded;

    protected bool facingRight = true;
    protected Animator animator;

    public MaterialColorEditor[] flashEditors;
    public Color[] startColors;

    protected virtual void Start() {
        animator = GetComponent<Animator>();
        // slow + generates garbage, OK to do on startup
        flashEditors = GetComponentsInChildren<MaterialColorEditor>(true)
            .Union(
                GetComponents<MaterialColorEditor>()
            )
            .Where(x => x.valueName.Equals("_FlashColor"))
            .ToArray();
        startColors = new Color[flashEditors.Length];
        for (int i=0; i<flashEditors.Length; i++) {
            startColors[i] = flashEditors[i].color;
        }
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
        if (animator != null) {
            animator.SetBool("Grounded", true);
        }
        grounded = true;
    }

    public virtual void OnGroundLeave() {
        if (animator != null) {
            animator.SetBool("Grounded", false);
        }
        grounded = false;
    }

    public virtual void Die() {
        Destroy();
    }

    public virtual void OnHit(Attack a) {
        DamageOutput d = a.CalculateDamage();
        DamageFor(d.rawAmount);
        if (d.critical) {
            DamageTextSpawner.WriteCrit(d.rawAmount.ToString(), transform.position);
        } else {
            DamageTextSpawner.WriteText(d.rawAmount.ToString(), transform.position);
        }
        FlashWhite();
    }

    public virtual void DamageFor(int amount) {
        hp -= amount;
        if (hp <= 0) {
            Die();
        }
    }

    public bool IsGrounded() {
        return grounded;
    }

    public virtual void OnLedgeStep() {
        
    }

    public bool IsFacingRight() {
        return facingRight;
    }

    public void FlashWhite(float duration=0.1f) {
        for (int i=0; i<flashEditors.Length; i++) {
            flashEditors[i].color = Color.white;
        }
        StartCoroutine(NormalColor(duration));
    }

    IEnumerator NormalColor(float duration) {
        yield return new WaitForSecondsRealtime(duration);
        for (int i=0; i<startColors.Length; i++) {
            flashEditors[i].color = startColors[i];
        }
    }
}

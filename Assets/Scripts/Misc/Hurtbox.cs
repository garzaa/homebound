using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour {

    [SerializeField]
    List<AttackType> sensitiveTo;

    Rigidbody2D parentBody;

    void Start() {
        parentBody = GetComponentInParent<Rigidbody2D>();
    }

    public bool HitBy(AttackType attackType) {
        return sensitiveTo.Contains(attackType);
    }

    public void OnHit(Attack attack) {
        if (attack.hasKnockback) {
            parentBody.velocity = attack.GetKnockback(this);
        }
        parentBody.GetComponent<Animator>().SetTrigger("Hurt");
    }
}

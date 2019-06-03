using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour {

    [SerializeField]
    List<AttackType> sensitiveTo;

    Rigidbody2D parentBody;
    Entity parentEntity;

    void Start() {
        parentBody = GetComponentInParent<Rigidbody2D>();
        parentEntity = GetComponentInParent<Entity>();
    }

    public bool HitBy(AttackType attackType) {
        return sensitiveTo.Contains(attackType);
    }

    public void OnHit(Attack attack) {
        if (attack.hasKnockback) {
            parentBody.velocity = attack.GetKnockback(this);
        }
        parentBody.GetComponent<Animator>().SetTrigger("Hurt");
        parentEntity.OnHit(attack);
    }
}

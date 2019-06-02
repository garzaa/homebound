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
        float scalar = (attack.transform.position.x < this.transform.position.x ? 1 : -1);
        parentBody.velocity = new Vector2(attack.knockback.x * scalar, attack.knockback.y);
        parentBody.GetComponent<Animator>().SetTrigger("Hurt");
    }
}

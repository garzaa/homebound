using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    
    public AttackType type;
    public int damage = 1;

    public int CalculateDamage() {
        return damage;
    }
}

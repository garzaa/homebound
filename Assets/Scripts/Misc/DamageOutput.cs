using UnityEngine;
using System.Collections;

public class DamageOutput : ScriptableObject {
    public readonly int rawAmount;
    public readonly bool critical;

    public DamageOutput(int rawAmount) {
        this.rawAmount = rawAmount;
        this.critical = false;
    }

    public DamageOutput(int rawAmount, bool critical) {
        this.rawAmount = rawAmount;
        this.critical = critical;
    }
}
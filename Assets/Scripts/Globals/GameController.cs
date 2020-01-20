using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    [HideInInspector]
    public static GameController gc;

    public bool whiteflash;
    public bool hitmarker;
    public bool hitstop;
    public bool shake;
    public bool damageNumbers;

    void Awake() {
        if (gc == null) gc = this;
        else Destroy(this.gameObject);
    }
}

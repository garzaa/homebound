using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour {
    public static GameObject currentPlayer;

    public GameObject initialPlayer;

    void Awake() {
        currentPlayer = initialPlayer;
    }
}

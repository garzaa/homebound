using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour {
    public static GameObject currentPlayer;

    public GameObject initialPlayer;
    PlayerFollower playerFollower;
    CameraOffset cameraOffset;

    void Awake() {
        currentPlayer = initialPlayer;
        playerFollower = GetComponentInChildren<PlayerFollower>();
        cameraOffset = GetComponentInChildren<CameraOffset>();
        ReactToPlayerChange();
    }

    void ReactToPlayerChange() {
        playerFollower.player = currentPlayer;
        cameraOffset.player = currentPlayer;
    }
}

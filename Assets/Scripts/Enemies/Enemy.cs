using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Entity {
	public GameObject playerObject;

    void Start() {
        playerObject = GameObject.Find("Player");
    }

}
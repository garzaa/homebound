﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    static readonly float INPUT_TOLERANCE = 0.1f;
    static readonly string HORIZONTAL = "Horizontal";

    public static bool HasHorizontalInput() {
        return Mathf.Abs(Input.GetAxis(HORIZONTAL)) > INPUT_TOLERANCE;
    }

    public static float HorizontalInput() {
        return Input.GetAxis(HORIZONTAL);
    }

}

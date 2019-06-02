using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererEditor : MonoBehaviour {
    protected LineRenderer line;

    protected virtual void Start() {
        line = GetComponent<LineRenderer>();
    }
}

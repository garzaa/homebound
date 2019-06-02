using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class LineRendererEditor : MonoBehaviour {
    protected LineRenderer line;

    [Range(0, 3)]
    public float thickness;
    private float thicknessLastFrame;

    protected virtual void Start() {
        line = GetComponent<LineRenderer>();
    }

    protected virtual void UpdateThickness() {
        if (thickness != thicknessLastFrame) {
            line.startWidth = thickness;
            line.endWidth = thickness;
        }
        thickness = thicknessLastFrame;
    }

}

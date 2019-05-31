using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimiter : MonoBehaviour {
    Rigidbody2D rb2d;
    public float maxSpeedX;
    public float maxSpeedY;

    void FixedUpdate() {
        Vector2 newVec;
        newVec.x = rb2d.velocity.x > maxSpeedX ? maxSpeedX : rb2d.velocity.x;
        newVec.y = rb2d.velocity.y > maxSpeedY ? maxSpeedY : rb2d.velocity.y;
        rb2d.velocity = newVec;
    }
}

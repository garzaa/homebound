using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : Entity {

    Rigidbody2D rb2d;
    public int moveForce;

    override protected void Start() {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Move();
    }

    void Move() {
        animator.SetBool("HasHorizontalInput", InputManager.HasHorizontalInput());
        animator.SetFloat("XInput", InputManager.HorizontalInput());

        rb2d.AddForce(new Vector2(moveForce * InputManager.HorizontalInput(), rb2d.velocity.y));

        float direction = InputManager.HorizontalInput();
        if (rb2d.velocity.x < 0 && facingRight) {
            Flip();
        } else if (rb2d.velocity.x > 0 && !facingRight) {
            Flip();
        }
    }

}

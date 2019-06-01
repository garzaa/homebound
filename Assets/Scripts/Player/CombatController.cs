using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : Entity {

    Rigidbody2D rb2d;
    public int moveForce;
    public int jumpForce;

    override protected void Start() {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Move();
    }

    void Move() {
        UpdateInputs();
        rb2d.AddForce(new Vector2(moveForce * InputManager.HorizontalInput(), rb2d.velocity.y));

        float direction = InputManager.HorizontalInput();
        if (rb2d.velocity.x < 0 && facingRight) {
            Flip();
        } else if (rb2d.velocity.x > 0 && !facingRight) {
            Flip();
        }
        UpdateTriggers();
    }

    void UpdateInputs() {
        animator.SetBool("HasHorizontalInput", InputManager.HasHorizontalInput());
        animator.SetFloat("XInput", InputManager.HorizontalInput());
        animator.SetBool("MovingBackwards", rb2d.velocity.x * InputManager.HorizontalInput() < 0);
    }

    void UpdateTriggers() {
        if (InputManager.ButtonDown(Buttons.JUMP) && grounded) {
            animator.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
    }

}

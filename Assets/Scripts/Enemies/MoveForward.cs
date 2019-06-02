using UnityEngine;

public class MoveForward : MonoBehaviour {
    Entity e;
    Rigidbody2D rb2d;

    public float speed;
    public bool isForce;

    void Start() {
        e = GetComponent<Entity>();
        rb2d = e.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        float newSpeed = speed * e.ForwardScalar();
        if (isForce) {
            rb2d.AddForce(new Vector2(newSpeed, 0));
        } else {
            rb2d.velocity = new Vector2(newSpeed, rb2d.velocity.y);
        }
    }
}
using UnityEngine;

public class RigidBodyForcer : RigidBodyAffector {
    public Vector2 force;
    public bool entityForward;

    private Entity e;

    override protected void Enter() {
        if (entityForward) {
            e = rb2d.GetComponent<Entity>();
        }
    }

    override protected void Update() {
        Vector2 f;
        if (entityForward) {
            Debug.Log(e.ForwardScalar());
            f = new Vector2(force.x * e.ForwardScalar(), force.y);
        } else {
            f = new Vector2(force.x, force.y);
        }
        Debug.Log(f);
        rb2d.AddForce(f);
    }
}
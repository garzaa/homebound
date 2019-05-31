using UnityEngine;

public class RigidBodyForcer : RigidBodyAffector {
    public Vector2 force;
    public bool entityForward;

    private Entity e;

    override protected void Enter() {
        base.Enter();
        if (entityForward) {
            e = rb2d.GetComponent<Entity>();
        }
    }

    override protected void Update() {
        Vector2 f = force;
        if (entityForward) {
            f *= new Vector2(e.ForwardScalar(), 1);
        }
        rb2d.AddForce(f);
    }
}
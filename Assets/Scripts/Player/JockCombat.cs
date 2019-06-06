using UnityEngine;

public class JockCombat : CombatController {
    public GameObject baseball;
    public Vector2 baseballVelocity;
    public Transform baseballCastPoint;

    override protected void UpdateTriggers() {
        base.UpdateTriggers();
        if (InputManager.ButtonDown(Buttons.SPECIAL)) {
            LaunchBaseball();
        }
    }

    void LaunchBaseball() {
        var b = Instantiate(baseball, baseballCastPoint.position, Quaternion.identity, null);
        Vector2 v = baseballVelocity;
        v.x *= this.transform.localScale.x;
        b.GetComponent<Rigidbody2D>().velocity = v;
        // don't leave the baseball behind when moving
        b.GetComponent<Rigidbody2D>().velocity += GetComponent<Rigidbody2D>().velocity;
    }
}
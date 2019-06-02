using UnityEngine;

public class Spinner : MonoBehaviour {
    public float speed;

    void FixedUpdate() {
        Vector3 r = this.transform.rotation.eulerAngles;
        r.z = (r.z + speed) % 360;
        this.transform.rotation = Quaternion.Euler(r);
    }
}
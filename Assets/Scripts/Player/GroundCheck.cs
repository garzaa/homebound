using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public Entity parentEntity;

	public GameObject corner1;
	public GameObject corner2;
	public GameObject currentGround;

	bool groundedCurrentFrame;
	bool ledgeStepCurrentFrame;

    void Start() {
        if (parentEntity != null) {
            parentEntity = GetComponentInParent<Entity>();
        }
    }  

	bool LeftGrounded() {
		Debug.DrawLine(corner1.transform.position + new Vector3(0, 0.1f, 0), corner1.transform.position);
		RaycastHit2D hit = Physics2D.Linecast(corner1.transform.position + new Vector3(0, 0.1f, 0), corner1.transform.position, 1 << LayerMask.NameToLayer(Layers.Ground));
		if (hit) {
			currentGround = hit.collider.gameObject;
		}
		return hit;
	}

	bool RightGrounded() {
		Debug.DrawLine(corner2.transform.position + new Vector3(0, 0.1f, 0), corner2.transform.position);
		return Physics2D.Linecast(corner2.transform.position + new Vector3(0, 0.1f, 0), corner2.transform.position, 1 << LayerMask.NameToLayer(Layers.Ground));
	}

	public bool IsGrounded() {
		return LeftGrounded() || RightGrounded();
	}

	public bool OnLedge() {
		return LeftGrounded() ^ RightGrounded();
	}

	void Update() {
		bool groundedLastFrame = groundedCurrentFrame;
		groundedCurrentFrame = IsGrounded();
		if (!groundedLastFrame && groundedCurrentFrame) {
			parentEntity.OnGroundHit();	
		} else if (groundedLastFrame && !groundedCurrentFrame) {
			parentEntity.OnGroundLeave();
		}

		if (parentEntity != null) {
			bool ledgeStepLastFrame = ledgeStepCurrentFrame;
			ledgeStepCurrentFrame = OnLedge();
			if (!ledgeStepLastFrame && ledgeStepCurrentFrame) {
				parentEntity.OnLedgeStep();
			}
		}
	}

	public EdgeCollider2D TouchingPlatform() {
		int layerMask = 1 << LayerMask.NameToLayer(Layers.Ground);
		RaycastHit2D g1 = Physics2D.Raycast(corner1.transform.position + new Vector3(0, .2f), Vector3.down, 1f, layerMask);
		RaycastHit2D g2 = Physics2D.Raycast(corner2.transform.position + new Vector3(0, .2f), Vector3.down, 1f, layerMask);
		if (g1.transform == null && g2.transform == null) {
			//return early to avoid redundant checks
			return null;
		}
		bool grounded1 = false;
		bool grounded2 = false;
		
		if (g1.transform != null) {
			grounded1 = g1.transform.gameObject.GetComponent<PlatformEffector2D>() != null;
		}
		if (g2.transform != null) {
			grounded2 = g2.transform.gameObject.GetComponent<PlatformEffector2D>() != null;
		}
		
		if (grounded1 && grounded2) {
			return g2.transform.gameObject.GetComponent<EdgeCollider2D>();
		}
		return null;
	}
}

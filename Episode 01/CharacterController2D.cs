using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class CharacterController2D : MonoBehaviour {

	public LayerMask collisionMask;

	const float skinWidth = .015f;

	int horizontalRayCount = 4;
	int verticalRayCount = 4;
	float horizontalRaySpacing;
	float verticalRaySpacing;

	BoxCollider2D collider;
	RaycastOrigins raycastOrigins;

	void Awake() {
		GetComponent<Rigidbody2D> ().isKinematic = true;
		collider = GetComponent<BoxCollider2D> ();

		CalculateRaySpacing ();
	}

	public void Move(Vector3 velocity) {
		UpdateRaycastOrigins ();

		// Check for collisions
		if (velocity.y != 0) {
			VerticalCollisions(ref velocity);
		}

		// Move character
		transform.Translate(velocity);
	}

	void VerticalCollisions(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = ((directionY < 0)?raycastOrigins.bottomLeft:raycastOrigins.topLeft) + Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.up * directionY, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
			if (hit) {
				velocity.y = hit.point.y - rayOrigin.y;
				rayLength = Mathf.Abs(velocity.y);
				velocity.y -= skinWidth * directionY;

			}
		}
	}

	void UpdateRaycastOrigins() {
		Bounds bounds = collider.bounds;
		bounds.Expand (-2 * skinWidth);

		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}


	void CalculateRaySpacing() {
		Bounds bounds = collider.bounds;
		bounds.Expand (-2 * skinWidth);

		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}
}

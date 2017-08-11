using UnityEngine;
using System.Collections;
using System;

public class RigidBodyObjectDrop : MonoBehaviour {

	// Rigid body, constraints and bool to check if arm is attached.
	Rigidbody rigidBody;
	RigidbodyConstraints originalConstraints;
	bool isAttached;

	// Position and rotation vectors.
	private Vector3 originalScale;
	private Vector3 objectGlobalPos;
	private Vector3 objectLocalPos;

	private GameObject player;


	// Use this for initialization
	void Start () {
        System.Console.WriteLine("Starting..");
		rigidBody = gameObject.GetComponent<Rigidbody> ();
		originalConstraints = rigidBody.constraints;

		isAttached = true;

		// Set object scale.
		originalScale = gameObject.transform.localScale;
		// Set local position.
		objectLocalPos = gameObject.transform.localPosition;

		// Obtain parent player object.
		player = GameObject.Find ("BennyBones");
	}
	
	// Update is called once per frame
	void Update () {
		// On e key press.
		if (Input.GetKeyDown (KeyCode.E)) {
			// Check if object is attached.
			if (isAttached == true) {
				DeattachObject();
			}
		}

		if (Input.GetKeyUp (KeyCode.R)) {
			if (isAttached == false) {
				// Check distance between objects position and current parent position.
				var distance = objectGlobalPos - player.transform.position;

				// y is set to normal height difference.
				if (Mathf.Abs(distance.x) < 3 && Mathf.Abs(distance.z) < 3 && Mathf.Abs(distance.y) < 3.093745f) {
					AttachObject();
				}
			}
		}
	}

	void AttachObject() {
		// Freeze arm.
		FreezeObject();

		// Attach object to parent.
		// Conform rotation and transform to parent, and obtain original scale values for same reason (otherwise object skews).
		gameObject.transform.parent = player.transform;
		gameObject.transform.rotation = player.transform.rotation;
		gameObject.transform.localScale = originalScale;

		// Transform position.
		gameObject.transform.localPosition = new Vector3 (objectLocalPos.x, objectLocalPos.y, objectLocalPos.z);

		isAttached = true;
	}

	void DeattachObject() {
		// Deattach object from parent.
		gameObject.transform.parent = null;
		isAttached = false;

		// Store new object pos.
		objectGlobalPos = gameObject.transform.position;

		// Unfreeze object.
		UnfreezeObject ();
	}

	void FreezeObject () {
		// Add original constraints to freeze the object.
		rigidBody.constraints = originalConstraints;
	}

	void UnfreezeObject () {
		// Remove frozen constrains so object will drop.
		rigidBody.constraints &= RigidbodyConstraints.None;
	}
}

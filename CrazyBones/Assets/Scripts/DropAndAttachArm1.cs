using UnityEngine;
using System.Collections;
using System;

public class DropAndAttachArm1 : MonoBehaviour {

	// Check whether object can be reattached.
	private bool isAttached = true;
	private Vector3 originalScale;
	private Vector3 objectGlobalPos;
	private Vector3 objectLocalPos;
	private GameObject player;

	// Use this for initialization
	void Start () {
		// Set object scale.
		originalScale = gameObject.transform.localScale;
		// Set local position.
		objectLocalPos = gameObject.transform.localPosition;

		// Obtain parent player object.
		player = GameObject.Find ("GameObject");
	}
	
	// Update is called once per frame
	void Update () {
		// On key press E drop the object.
		if (Input.GetKeyDown (KeyCode.E)) {
			if (isAttached == true) {
				// Set attached variable.
				isAttached = false;

				// Deattach object from parent.
				gameObject.transform.parent = null;
				// Set GLOBAL position of object to it's local x and z position, and global position of 1.
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);

				// Store new object pos.
				objectGlobalPos = gameObject.transform.position;
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			if (isAttached == false) {
				// Check distance between objects position and current parent position.
				var distance = objectGlobalPos - player.transform.position;

				// y is set to normal height difference.
				if (distance.x < 3 && distance.z < 3 && distance.y < 3.093745f) {
					// Attach object to parent.
					// Conform rotation and transform to parent, and obtain original scale values for same reason (otherwise object skews).
					gameObject.transform.parent = player.transform;
					gameObject.transform.rotation = player.transform.rotation;
					gameObject.transform.localScale = originalScale;

					// Transform position.
					gameObject.transform.localPosition = new Vector3 (objectLocalPos.x, objectLocalPos.y, objectLocalPos.z);

					// Set attached variable.
					isAttached = true;	
				} else {
					// Object too far. Print to screen maybe
				}
			}
		}
	}
}

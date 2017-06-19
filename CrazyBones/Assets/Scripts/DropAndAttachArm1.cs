using UnityEngine;
using System.Collections;
using System;

public class DropAndAttachArm1 : MonoBehaviour {

	// Check whether arm can be reattached.
	private bool isAttached = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// On key press E drop the arm.
		if (Input.GetKeyDown (KeyCode.E)) {
			if (isAttached == true) {
				// Set attached variable.
				isAttached = false;

				// Deattach arm from parent.
				gameObject.transform.parent = null;
				// Set GLOBAL position of arm to it's local x and z position, and global position of 1.
				gameObject.transform.position = new Vector3(gameObject.transform.localPosition.x, 1, gameObject.transform.localPosition.z);
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			if (isAttached == false) {
				// Attach arm to parent.
				var player = GameObject.Find("GameObject").transform;
				gameObject.transform.parent = player;

				// Transform position.
				gameObject.transform.localPosition = new Vector3(0, 0, 3.093745f);

				// Set attached variable.
				isAttached = true;
			}
		}
	}
}

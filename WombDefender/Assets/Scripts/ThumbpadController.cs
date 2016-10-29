using UnityEngine;
using System.Collections;

public class ThumbpadController : MonoBehaviour {

	public GameObject thumbpad;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) { // If the screen is being touched
			// Detect whether touch is within the object

			Touch touch = Input.GetTouch (0); // Assume only single-touch for now
			Vector3 worldCoords = Camera.main.ScreenToWorldPoint (touch.position); // Turn 2D point on screen into 3D position in the "game world"
			Vector2 world2DCoords = new Vector2 (worldCoords.x, worldCoords.y); // Discard the z-coordinate, everything's at z = 0
			RaycastHit2D hitInfo = Physics2D.Raycast (world2DCoords, Camera.main.transform.forward); // Trace ray back from hit point to camera, see if we hit anything

			// Now check whether the touch hit an object, and if so whether it's the thumbpad track. Only then can we proceed
			if (hitInfo.collider != null && hitInfo.transform.name == "control_track") {
				thumbpad.transform.localPosition = new Vector3 (
					(worldCoords.x - hitInfo.transform.position.x) / 4,
					(worldCoords.y - hitInfo.transform.position.y) / 4,
					0); // z-coordinate must remain at 0 to keep things visible!

				Vector2 referenceVector = new Vector2 (0.1f, -0.1f); // Reference vector; 45 degrees toward the bottom-right
				Vector2 thumbpadVector = new Vector2 (thumbpad.transform.localPosition.x, thumbpad.transform.localPosition.y); // Thumbpad point

				float targetAngle = Vector2.Angle (referenceVector, thumbpadVector); // Dot product; gives absolute angle between the two
				Vector3 cross = Vector3.Cross (referenceVector, thumbpadVector);
				if (cross.z < 0) { // If cross product points away from camera then the angle is over 180 degrees. Source: Unity answers :-P
					targetAngle = 360 - targetAngle;
				}


				GameObject.Find ("barrier_parent").GetComponent<BarrierController> ().TargetAngle = targetAngle;
			}

		} else { // Let go to stop moving immediately
			GameObject.Find ("barrier_parent").GetComponent<BarrierController> ().TargetAngle = 
				GameObject.Find ("barrier_parent").transform.eulerAngles.z;

			thumbpad.transform.localPosition = new Vector3 (0, 0, 0); // Snap the thumbpad back to the centre of the track
		}
	}
}

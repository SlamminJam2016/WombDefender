using UnityEngine;
using System.Collections;

public class BarrierController : MonoBehaviour {

	private const int ANGLE_FACTOR = 100;

	public float TargetAngle = 0; // Angle toward which we rotate with the thumbpad

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Keyboard controls -- for desktop only!
		if (Input.GetKey ("left")) {
			transform.Rotate (Vector3.back * Time.deltaTime * ANGLE_FACTOR);
			TargetAngle = transform.eulerAngles.z; // Override the thumbpad; arrow keys are supposed to be for debug only
		} else if (Input.GetKey ("right")) {
			transform.Rotate (Vector3.forward * Time.deltaTime * ANGLE_FACTOR);
			TargetAngle = transform.eulerAngles.z; // Override the thumbpad; arrow keys are supposed to be for debug only
		}

		// Thumbpad controls are done in ThumbpadController and affect the TargetAngle here
		if (transform.eulerAngles.z != TargetAngle) {
			// Figure out the more efficient direction to turn in, then rotate the barrier in that direction
			// TODO: Do this in like 3 lines instead of 15, also label the variables better
			float angleDifference1 = transform.eulerAngles.z - TargetAngle;
			float angleDifference2 = -angleDifference1;

			if (angleDifference1 < 0) {
				angleDifference1 += 360;
			}
			if (angleDifference2 < 0) {
				angleDifference2 += 360;
			}

			if (angleDifference1 < angleDifference2) {
				transform.Rotate (Vector3.back * Time.deltaTime * ANGLE_FACTOR);
			} else {
				transform.Rotate (Vector3.forward * Time.deltaTime * ANGLE_FACTOR);
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class BarrierController : MonoBehaviour {

	private const int ANGLE_FACTOR = 50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// CONTROLS


		// Keyboard controls -- for desktop only!
		if (Input.GetKey ("left")) {
			transform.Rotate (Vector3.back * Time.deltaTime * ANGLE_FACTOR);
		} else if (Input.GetKey ("right")) {
			transform.Rotate (Vector3.forward * Time.deltaTime * ANGLE_FACTOR);
		}
			
		// TODO: Mobile controls -- send events from virtual joystick object?



		// RENDERING

		// We rotate the barrier's parent object to make it turn around the womb's center

	}
}

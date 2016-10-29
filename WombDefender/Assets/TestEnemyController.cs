using UnityEngine;
using System.Collections;

public class TestEnemyController : MonoBehaviour {

	private const int BASE_SPEED = 1;

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Move towards womb
		float step = BASE_SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") { // ie. collision with the barrier
			Destroy(gameObject);
		}
	}
}

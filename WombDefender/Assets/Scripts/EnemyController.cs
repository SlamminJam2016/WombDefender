using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public enum EnemyType {Normal, Tough, Tricky, Potent, Boss};

	private const int BASE_SPEED = 1;

	public EnemyType type;

	private int hits; // How many hits this can take

	// Use this for initialization
	void Start () {
		hits = type == EnemyType.Boss ? 5 : (type == EnemyType.Tough ? 2 : 1);
	}
	
	// Update is called once per frame
	void Update () {
		// Move towards womb
		float step = BASE_SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, GameObject.Find ("womb").transform.position, step);
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") { // ie. collision with the barrier
			hits--;
			if (hits == 0) {
				Destroy (gameObject);
			} else {
				// TODO: Behavior to bounce off shield and try again
			}
		}

		if (coll.gameObject.name == "womb") {
			GameObject.Find ("womb").GetComponent<WombController> ().currentHealth--;
			Destroy (gameObject);
		}
	}
}

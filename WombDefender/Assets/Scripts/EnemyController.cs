using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public enum EnemyType {Normal, Tough, Tricky, Potent, Boss};

	private const int BASE_SPEED = 1;

	public EnemyType type;

	private int hits; // How many hits this can take

	public int scoreValue = 1;

	public Sprite tough_sprite;
	public Sprite tricky_sprite;
	public Sprite potent_sprite;
	public Sprite boss_sprite;

	// Use this for initialization
	void Start () {
		// Ensure that enemies go right through the thumbpad
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), GameObject.Find ("control_track").GetComponent<Collider2D> ());
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
				ScoreController.score += scoreValue;
				ScoreController.hscore += 1; // This is effectively a killcount
			} else {
				// TODO: Behavior to bounce off shield and try again
			}
		}

		if (coll.gameObject.name == "womb") {
			GameObject.Find ("womb").GetComponent<WombController> ().currentHealth--;

			Destroy (gameObject);
		}
	}

	// Since we have to assign type after creating the object, we can't do this stuff in Start()
	public void SetupType (EnemyType e) {
		type = e;

		// Change image based on enemy type
		if (type == EnemyType.Boss) {
			GetComponent<SpriteRenderer> ().sprite = boss_sprite;
		} else if (type == EnemyType.Potent) {
			GetComponent<SpriteRenderer> ().sprite = potent_sprite;
		} else if (type == EnemyType.Tricky) {
			GetComponent<SpriteRenderer> ().sprite = tricky_sprite;
		} else if (type == EnemyType.Tough) {
			GetComponent<SpriteRenderer> ().sprite = tough_sprite;
		} // Normal enemies just get the normal sprite

		//hits = type == EnemyType.Boss ? 5 : (type == EnemyType.Tough ? 2 : 1); // Set higher HP for these enemy types
		hits = 1;
	}
}

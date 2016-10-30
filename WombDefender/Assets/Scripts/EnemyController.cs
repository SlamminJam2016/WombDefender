using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public enum EnemyType {Normal, Tough, Tricky, Potent, Boss};

	private float base_speed;

	public EnemyType type;

	private int hits; // How many hits this can take
	private bool is_stunned; // For multi-hit enemies, set to true while it's stunned

	public int scoreValue;

	public Sprite tough_sprite;
	public Sprite tricky_sprite;
	public Sprite potent_sprite;
	public Sprite boss_sprite;

	// Use this for initialization
	void Start () {
		// Ensure that enemies go right through the thumbpad
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), GameObject.Find ("control_track").GetComponent<Collider2D> ());

		// Probably not the most efficient way, but makes sure enemies don't collide with each other
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), enemy.GetComponent<Collider2D> ());
		}
	}

	// Update is called once per frame
	void Update () {
		float step = base_speed * Time.deltaTime * (is_stunned ? -1f : 1f); // If stunned, move backward
		transform.position = Vector3.MoveTowards (transform.position, GameObject.Find ("womb").transform.position, step);

		// TODO: Make it spin while stunned
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") { // ie. collision with the barrier
			hits--;
			if (hits == 0) {
				Destroy (gameObject);
				ScoreController.score += scoreValue;
				ScoreController.totalScore += scoreValue; // used for shop purposes
				ScoreController.hscore += 1; // This is effectively a killcount
			} else {
				is_stunned = true;
				Physics2D.IgnoreCollision (GetComponent<Collider2D> (), GameObject.Find ("barrier").GetComponent<Collider2D> ());
				StartCoroutine(Unstun(5f));
			}
		}

		if (coll.gameObject.name == "womb") {
			GameObject.Find ("womb").GetComponent<WombController> ().currentHealth -= 
				(type == EnemyType.Potent && GameObject.Find ("womb").GetComponent<WombController> ().currentHealth > 1 ? 2 : 1);

			Destroy (gameObject);
		}
	}

	// Since we have to assign type after creating the object, we can't do this stuff in Start()
	public void SetupType (EnemyType e) {
		type = e;

		if (type == EnemyType.Boss) {
			GetComponent<SpriteRenderer> ().sprite = boss_sprite;
			base_speed = 0.4f;
			hits = 3;
			scoreValue = 500;
		} else if (type == EnemyType.Potent) {
			GetComponent<SpriteRenderer> ().sprite = potent_sprite;
			base_speed = 1.1f;
			hits = 1;
			scoreValue = 150;
		} else if (type == EnemyType.Tricky) {
			GetComponent<SpriteRenderer> ().sprite = tricky_sprite;
			base_speed = 0.8f;
			hits = 1;
			scoreValue = 150;
		} else if (type == EnemyType.Tough) {
			GetComponent<SpriteRenderer> ().sprite = tough_sprite;
			base_speed = 0.6f;
			hits = 2;
			scoreValue = 200;
		} else {
			// Normal enemies just get the normal sprite, don't set it
			base_speed = 0.7f;
			hits = 1;
			scoreValue = 50;
		}

		//hits = type == EnemyType.Boss ? 5 : (type == EnemyType.Tough ? 2 : 1); // Set higher HP for these enemy types

		is_stunned = false;
	}

	IEnumerator Unstun(float time) {
		yield return new WaitForSeconds (time); // Stop this coroutine until this amount of time passes

		is_stunned = false;
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), GameObject.Find ("barrier").GetComponent<Collider2D> (), false);
	}
}

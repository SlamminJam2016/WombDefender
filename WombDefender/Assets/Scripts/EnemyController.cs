using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public enum EnemyType {Normal, Tough, Tricky, Potent, Boss};

	private float base_speed;

	public EnemyType type;

	private int hits; // How many hits this can take
	private bool is_stunned; // For multi-hit enemies, set to true while it's stunned

	public int scoreValue;

	public RuntimeAnimatorController normal_animator;
	public RuntimeAnimatorController tough_animator;
	public RuntimeAnimatorController tricky_animator;
	public RuntimeAnimatorController potent_animator;
	public RuntimeAnimatorController boss_animator;

	private string animation_name;

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
		// Movement
		float step = base_speed * Time.deltaTime * (is_stunned ? -1f : 1f); // If stunned, move backward
		Vector3 direction;
		if (type != EnemyType.Tricky) {
			direction = GameObject.Find ("womb").transform.position - transform.position;
		} else { // "Tricky" enemies zigzag a bit
			// Deviate up to 60 degrees left or right, depending on time. Should be a steady cycle.
			float angle = 60 * Mathf.Sin((Time.time % 3f) * (2 * Mathf.PI) / 3); // Should cycle within [-60, 60] once per second
			direction = Quaternion.Euler (0, 0, angle) * (GameObject.Find ("womb").transform.position - transform.position);
		}
		Vector3 targetPosition = transform.position + direction;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);

		// Facing
		if (is_stunned) { // Spin while stunned
			transform.Rotate (Vector3.forward * Time.deltaTime * 350);
		} else { // Face in the direction it's moving
			float angle = Vector3.Angle (Vector3.right, direction); // Vector3.right is the default orientation of the sprite
			if (direction.y < 0) { // Since we have the non-reflex angle but will always rotate counterclockwise, this finds the actual angle
				angle = 360 - angle;
			}
			transform.eulerAngles = new Vector3 (0, 0, angle);
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") { // ie. collision with the barrier
			hits--;
			if (hits == 0) {
				Destroy (gameObject);
				ScoreController.score += scoreValue;
				ScoreController.hscore += 1; // This is effectively a killcount
			} else {
				is_stunned = true;
				Physics2D.IgnoreCollision (GetComponent<Collider2D> (), GameObject.Find ("barrier").GetComponent<Collider2D> ());

				// Freeze animation
				GetComponentInChildren<Animator> ().speed = 0;
				GetComponentInChildren<Animator> ().Play(animation_name, 0, 0.5f);

				StartCoroutine(Unstun(5f)); // Unstun in 5 seconds
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
			GetComponentInChildren<Animator> ().runtimeAnimatorController = boss_animator;
			base_speed = 0.4f;
			hits = 3;
			scoreValue = 500;
			animation_name = "sperm_boss";
		} else if (type == EnemyType.Potent) {
			GetComponentInChildren<Animator> ().runtimeAnimatorController = potent_animator;
			base_speed = 1.1f;
			hits = 1;
			scoreValue = 150;
			animation_name = "sperm_potent";
		} else if (type == EnemyType.Tricky) {
			GetComponentInChildren<Animator> ().runtimeAnimatorController = tricky_animator;
			base_speed = 0.8f;
			hits = 1;
			scoreValue = 150;
			animation_name = "sperm_tricky";
		} else if (type == EnemyType.Tough) {
			GetComponentInChildren<Animator> ().runtimeAnimatorController = tough_animator;
			base_speed = 0.6f;
			hits = 2;
			scoreValue = 200;
			animation_name = "sperm_tough";
		} else {
			GetComponentInChildren<Animator> ().runtimeAnimatorController = normal_animator;
			base_speed = 0.7f;
			hits = 1;
			scoreValue = 50;
			animation_name = "sperm_normal";
		}

		is_stunned = false;
		GetComponentInChildren<Animator> ().speed = 0.7f;
	}

	IEnumerator Unstun(float time) {
		yield return new WaitForSeconds (time); // Stop this coroutine until this amount of time passes

		is_stunned = false;
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), GameObject.Find ("barrier").GetComponent<Collider2D> (), false);
		GetComponentInChildren<Animator> ().speed = 0.7f;
	}
}

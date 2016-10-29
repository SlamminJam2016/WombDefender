using UnityEngine;
using System.Collections;

public class WombController : MonoBehaviour {

	// health stats
	public int currentHealth;
	public int maxHealth = 5;


	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;

	}
	
	// Update is called once per frame
	void Update () {

		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;

		} 

		if (ScoreScript.hscore >= 100) {
			if (currentHealth < maxHealth) {
				currentHealth += 1;
			}
			ScoreScript.hscore -= 100;
		
		}

		if(currentHealth <= 0) {
			Die ();

		}
	
	}

	void Die() {
		// restart game on death Note: change later to screen/tap to start
		//SceneManager.LoadScene(SceneManager.LoadedScene);
	}
}

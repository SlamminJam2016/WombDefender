using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WombController : MonoBehaviour {

	// health stats
	public int currentHealth;
	public int maxHealth = 5;

	private bool extra_life_flag; // When true, award an extra life at next multiple of 20 kills


	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		extra_life_flag = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;

		} 

		if (ScoreController.hscore % 20 == 0 && extra_life_flag) {
			if (currentHealth < maxHealth) {
				currentHealth += 1;
			}
			extra_life_flag = false;
		} else if (ScoreController.hscore % 20 != 0 && !extra_life_flag) {
			extra_life_flag = true;
		}

		if(currentHealth <= 0) {
			Die ();

		}
	
	}

	void Die() {
		
		// restart game on death Note: change later to screen/tap to start
		SceneManager.LoadScene("GameOver");
	}
}

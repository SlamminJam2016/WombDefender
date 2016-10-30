using UnityEngine;
using System.Collections; 

public class EnemySpawnController : MonoBehaviour {

	// Odds of each following enemy being a particular type
	private int[] enemy_type_prob;

	// Parameters for random wait interval before next spawn
	private float min_wait;
	private float max_wait;

	// Could have done this all internally but setting it through the UI seems easier
	public GameObject enemy_prefab;

	// Flags used to control when waiting time gets shortened and when enemy type probabilities change
	private bool wait_flag;
	private bool enemy_prob_flag;

	// Use this for initialization
	void Start () {
		// To start we spawn only normal enemies
		enemy_type_prob = new int[5];
		enemy_type_prob [0] = 100;
		enemy_type_prob [1] = 0;
		enemy_type_prob [2] = 0;
		enemy_type_prob [3] = 0;
		enemy_type_prob [4] = 0;

		// Starting time interval: 1.5 to 2.5 second range
		min_wait = 1500;
		max_wait = 2500;

		// Set flags to starting values
		wait_flag = false;

		Invoke ("Spawn", Random.Range (min_wait, max_wait + 1) / 1000);
	}
	
	// Update is called once per frame
	void Update () {
		// Change enemy type probability and waiting time range based on player score
		if (ScoreController.hscore % 15 == 0 && min_wait >= 600 && wait_flag) { // Decrease wait time a little bit every 10 kills (enemy type irrelevant)
			min_wait -= 75;
			max_wait -= 125;

			wait_flag = false; // So that it doesn't constantly decrease with each frame
		} else if (ScoreController.hscore % 15 != 0 && !wait_flag) {
			wait_flag = true; // Get ready for the next 10
		}

		if (ScoreController.hscore % 5 == 0 && enemy_type_prob[0] > 20 && enemy_prob_flag) {
			enemy_type_prob [0] -= 10; // Decrement normal enemies
			enemy_type_prob [1] += 4;
			enemy_type_prob [2] += 2;
			enemy_type_prob [3] += 3;
			enemy_type_prob [4] += 1;

			enemy_prob_flag = false; // So that it doesn't constantly change with each frame
		} else if (ScoreController.hscore % 5 != 0 && !enemy_prob_flag) {
			enemy_prob_flag = true; // Get ready for the next 5
		}
	}

	// Spawn an enemy with random position and type, after a random amount of time
	void Spawn () {
		// Determine position
		int side = Random.Range(0, 2); // Coinflip for left vs right
		float yCoord = (float)(Random.Range(0, 21) * 0.5 - 5);
		float xCoord = (float)(side == 0 ? -8 : 8);

		// Determine type
		// TODO: Again, make this not take a bajillion lines
		int typeRoll = Random.Range(1, 101); // in [1, 100]
		int typeMap = 0;
		for (int i = 0; i < 5; i++) { // Loop through the enemy types; higher roll means stronger enemy
			typeRoll -= enemy_type_prob[i];
			if (typeRoll <= 0) { // We have our type
				typeMap = i;
				break;
			}
		}
		EnemyController.EnemyType type;
		switch (typeMap) {
		case 4:
			type = EnemyController.EnemyType.Boss;
			break;
		case 3:
			type = EnemyController.EnemyType.Potent;
			break;
		case 2:
			type = EnemyController.EnemyType.Tricky;
			break;
		case 1:
			type = EnemyController.EnemyType.Tough;
			break;
		case 0:
		default:
			type = EnemyController.EnemyType.Normal;
			break;
		}

		// Spawn the enemy
		GameObject newEnemy = (GameObject)Instantiate(enemy_prefab, new Vector3(xCoord, yCoord, 0), Quaternion.identity);
		newEnemy.GetComponent<EnemyController> ().SetupType (type);


		// Prepare to spawn the next enemy
		// TODO: Maybe add a check that the player is not dead?
		Invoke ("Spawn", Random.Range (min_wait, max_wait + 1) / 1000);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shopController : MonoBehaviour {

	public void condomBuy() {
		if (ScoreController.totalScore >= 1000) {
			ScoreController.totalScore -= 1000;
			TapToStartScript.condomCount += 1;
		}

	}

	public void spermicideBuy() {
		if (ScoreController.totalScore >= 2500) {
			ScoreController.totalScore -= 2500;
			TapToStartScript.spermicideCount += 1;
		}

	}
	public void planBBuy() {
		if (ScoreController.totalScore >= 2000) {
			ScoreController.totalScore -= 2000;
			TapToStartScript.planBCount += 1;
		}

	}

	public void barrierBuy() {
		if (ScoreController.totalScore >= 1000) {
			ScoreController.totalScore -= 1000;
			TapToStartScript.barrierCount += 1;
		}

	}

	public void pineappleBuy() {
		if (ScoreController.totalScore >= 100) {
			ScoreController.totalScore -= 100;
			TapToStartScript.pineappleCount += 1;
		}

	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

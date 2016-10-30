using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverOptions : MonoBehaviour {

	// Use this for initialization

	void Start () {
		//float fadeTime = GameObject.Find ("gameOverScreen").GetComponent<Fading> ().BeginFade (1);
	}

	// Update is called once per frame
	void Update () {
		if((Input.touchCount >= 1) || (Input.GetKey("left"))) {
			
			//yield return new WaitForSeconds (fadeTime);
			SceneManager.LoadScene("Shop");
		}

	}
}

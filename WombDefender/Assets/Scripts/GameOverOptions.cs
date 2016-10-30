using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverOptions : MonoBehaviour {

	// Use this for initialization

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if((Input.touchCount >= 1) || (Input.GetKey("left"))) {
			SceneManager.LoadScene("Shop");
		}

	}
}

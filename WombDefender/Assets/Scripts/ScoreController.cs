using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public static int score;
	public static int totalScore; // used for shop purposes
	// invisible counter that gives extra life
	public static int hscore;

	Text text;

	void Start () {

		text = GetComponent<Text>();
		score = 0;
		hscore = 0;
	
	}

	// Update is called once per frame
	void Update () {

		text.text = "" + score;

	
	}
}

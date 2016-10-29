using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour {

	public Sprite[] HeartSprites;

	public Image HeartUI;

	private Womb womb;


	// Use this for initialization
	void Start () {
		womb = GameObject.Find ("womb").GetComponent<Womb> ();

	}

	// Update is called once per frame
	void Update () {
		
		HeartUI.sprite = HeartSprites[womb.currentHealth];
	}


}

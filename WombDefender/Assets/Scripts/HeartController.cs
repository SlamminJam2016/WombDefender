using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeartController : MonoBehaviour {

	public Sprite[] HeartSprites;

	public Image HeartUI;

	private WombController womb;


	// Use this for initialization
	void Start () {
		womb = GameObject.Find ("womb").GetComponent<WombController> ();

	}

	// Update is called once per frame
	void Update () {
		
		HeartUI.sprite = HeartSprites[womb.currentHealth];

	}


}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TapToStartScript : MonoBehaviour {

	// Use this for initialization

	public static int condomCount = 0;
	public static int spermicideCount = 0;
	public static int planBCount = 0;
	public static int barrierCount = 0;
	public static int pineappleCount = 0;

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		if((Input.touchCount >= 1) || (Input.GetKey("left"))) {
			

			SceneManager.LoadScene("level_demo");
		}
	
	}
}

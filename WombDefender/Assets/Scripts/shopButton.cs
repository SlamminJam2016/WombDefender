using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class shopButton : MonoBehaviour {

	public void shop(int sceneToChangeTo) {
		
		SceneManager.LoadScene (sceneToChangeTo);

	}
}
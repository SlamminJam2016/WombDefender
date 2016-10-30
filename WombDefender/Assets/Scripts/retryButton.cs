using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class retryButton : MonoBehaviour {

	public void retry(int sceneToChangeTo) {
		
		SceneManager.LoadScene (sceneToChangeTo);

	}
}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class retryButton_b : MonoBehaviour {

	public void retry_b(int sceneToChangeTo) {
		
		SceneManager.LoadScene (sceneToChangeTo);

	}
}
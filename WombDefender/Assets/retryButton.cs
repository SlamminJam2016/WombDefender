using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class retryButton : MonoBehaviour {

	public void screenChange(int sceneToChangeTo) {

		SceneManager.LoadScene (sceneToChangeTo);

	}
}
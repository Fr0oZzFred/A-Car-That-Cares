using UnityEngine;
using UnityEngine.SceneManagement;
public class Managers : MonoBehaviour {

    void Start() {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}

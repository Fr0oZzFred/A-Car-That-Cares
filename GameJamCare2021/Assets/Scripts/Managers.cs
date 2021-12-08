using UnityEngine;
using UnityEngine.SceneManagement;
public class Managers : MonoBehaviour {

    void Start() {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Main");
        GameManager.Instance.ChangeGameState((int)GameManager.GameState.MainMenu);
    }
}

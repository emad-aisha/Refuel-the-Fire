using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    [Header("Start Game")]
    [SerializeField] string sceneName;


    bool ignore;
    public void StartGame() {
        SceneManager.LoadSceneAsync(sceneName);
    }


}

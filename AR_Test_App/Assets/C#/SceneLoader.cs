using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GoTo(string sceneName) => SceneManager.LoadScene(sceneName);
    public void CloseApp() => Application.Quit();
}

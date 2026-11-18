using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Scene to load when Play pressed")]
    public string gameSceneName = "Task1";   // must match your game scene's name

    public void OnPlayClick()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnQuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
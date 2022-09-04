using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int gameSceneIndex, tutorialIndex, menuIndex;

    private void Awake()
    {
        if( Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
    public void StartTutorial()
    {
        SceneManager.LoadScene(tutorialIndex);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(menuIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

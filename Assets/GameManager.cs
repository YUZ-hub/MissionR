using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject winPanel, losePanel;
    [SerializeField] private int gameSceneIndex;

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
    public void Win()
    {
        winPanel.SetActive(true);
    }
    public void Lose()
    {
        losePanel.SetActive(true);
    }
}

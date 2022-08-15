using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
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
        //load scene
    }
    public void GameOver()
    {
        //gameover ui
    }
    public void StageClear()
    {
        //win game ui
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        if (instance == null)
            instance = new GameObject("_GameManager").AddComponent<GameManager>();

        return instance;
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitGameManager();
    }

    private void InitGameManager()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            UIManager.GetInstance().ShowUI("TitleMenu");

        }

        if (SceneManager.GetActiveScene().name == "GameplayScene")
        {
            UIManager.GetInstance().ShowUI("GameplayMenu");

        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

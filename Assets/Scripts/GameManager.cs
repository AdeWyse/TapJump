using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameManagerObject;
    public int chosenLevel;

    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
    }
    public void SetLevel(int chosen)
    {
        chosenLevel = chosen - 1;

        CallGame();
    }

    void CallGame()
    {
        DontDestroyOnLoad(gameManagerObject);
        SceneManager.LoadSceneAsync("Game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameManagerObject;
    private PlayerInfoManager info;
    public int chosenLevel;

    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        if(PlayerPrefs.GetString("unity.player_session_count") == "1"){
            info = new PlayerInfoManager();
            info.Create();
       }
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

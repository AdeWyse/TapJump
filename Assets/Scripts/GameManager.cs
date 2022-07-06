using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*Script that is responsable for first run maintenance and the choice of witch level is going to be loaded on the Game Scene*/
public class GameManager : MonoBehaviour
{
    private GameObject gameManagerObject;
    private PlayerInfoManager info;
    public int chosenLevel;

    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        //Creates a clean save file on the first run of the game
        if(PlayerPrefs.GetString("unity.player_session_count") == "1"){
            info = new PlayerInfoManager();
            info.Create();
       }
    }
    //Used by the Title Screen buttons to decide wich level is going to be loaded and the calls the function the loads the next scene
    public void SetLevel(int chosen)
    {
        chosenLevel = chosen - 1;

        CallGame();
    }
    //Calls the Game Scene and makes sure that this script is going to be caried to said scene
    void CallGame()
    {
        DontDestroyOnLoad(gameManagerObject);
        SceneManager.LoadSceneAsync("Game");
    }
}

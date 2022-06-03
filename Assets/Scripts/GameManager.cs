using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameManagerObject;
    public  bool gameStatus = true;
    public  bool gameResult = false;
    public int atemptNumber = 0;
    public int chosenLevel;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set1()
    {
        chosenLevel = 0;
        CallGame();
    }

    public void Set2()
    {
        chosenLevel = 1;

        CallGame();
    }

    void CallGame()
    {
        DontDestroyOnLoad(gameManagerObject);
        SceneManager.LoadSceneAsync("Game");
    }
}

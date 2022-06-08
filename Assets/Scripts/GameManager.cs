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
    public void SetLevel(int chosen)
    {
        chosenLevel = chosen - 1;

        CallGame();
    }

    void CallGame()
    {
        DontDestroyOnLoad(gameManagerObject);
        atemptNumber += 1;
        SceneManager.LoadSceneAsync("Game");
    }
}

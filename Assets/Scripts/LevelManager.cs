using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private GameManager gameManager;
    private GameObject[] movingPartsBackground;
    private GameObject player;
    public GameObject course;
    private GameObject pauseScreen;
    private float vanishingpoint = -21f;
    private float initalPos = 25f;
    private float backgroundSpeed = 5f;
    public bool gameStatus;
    public bool gameResult;
    
    public int points;
    public int pointsToCount = 0;
    public bool paused = false;

    private int level;

    public int atemptNumber;
    public int score;


    private Vector2 playerInitialPos;
    private Vector2 courseInitialPos;
    private Vector2[] backgroundInitialPos;
    private GameObject[] coins;

    public GameObject[] courses;
    public AudioClip[] audios;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI atemptsText;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatus && !gameResult && !paused)
        {
            foreach (GameObject movingBackground in movingPartsBackground)
            {
                MoveBackground(movingBackground);
            }
            MoveObstacles(course);
            PointsCount(course);
        }
        else
        {
            if (paused)
            {
                course.GetComponent<AudioSource>().Pause();
            }
            else
            {
                if (!gameResult)
                {
                    course.GetComponent<AudioSource>().Stop();
                    RestartOnLose();
                }
                else
                {
                    score = points + pointsToCount;
                }
            }
        }
    }
    void Initialize()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        pauseScreen = GameObject.Find("Pause");
        pauseScreen.SetActive(false);
        movingPartsBackground = GameObject.FindGameObjectsWithTag("MovingBackground");
        backgroundInitialPos = new Vector2[movingPartsBackground.Length];

        gameStatus = true;
        gameResult = false;
        atemptNumber = PlayerPrefs.GetInt(level.ToString() + "atempts");
        Debug.Log(PlayerPrefs.HasKey(level.ToString() + "Atempts"));
        if(atemptNumber == null || atemptNumber == 0)
        {
            atemptNumber = 1;
        }
        level = gameManager.chosenLevel;
        course = Instantiate(courses[level], new Vector3(-2.3f, -4f, 0f), new Quaternion(0, 0, 0, 0));
        course.AddComponent<AudioSource>().clip = audios[level];
        course.GetComponent<AudioSource>().Play();
        course.SetActive(true);
        coins = GameObject.FindGameObjectsWithTag("Coin");

        GetInitialPos();
    }

    void MoveBackground(GameObject movingBackground)
    {
        Vector2 direction = new Vector2(-40f, movingBackground.transform.position.y);
        movingBackground.transform.position = Vector2.MoveTowards(movingBackground.transform.position, direction, backgroundSpeed * Time.deltaTime);
        if (movingBackground.transform.position.x < vanishingpoint)
        {
            movingBackground.transform.position = new Vector2(initalPos, movingBackground.transform.position.y);
        }
        return;
    }

    void MoveObstacles(GameObject course)
    {

        Vector2 direction = new Vector2(-220f, course.transform.position.y);
        course.transform.position = Vector2.MoveTowards(course.transform.position, direction, backgroundSpeed * Time.deltaTime);
        if (course.transform.position.x <= -135.2f)
        {
            gameStatus = false;
            gameResult = true;
        }
    }


    private void GetInitialPos()
    {
        playerInitialPos = player.transform.position;
        courseInitialPos = course.transform.position;
        int i = 0;
        foreach (GameObject movingBackground in movingPartsBackground)
        {
            backgroundInitialPos[i] = movingBackground.transform.position;
            i++;
        }
        return;
    }


    private void RestartOnLose()
    {
        //Moves everthing to the initial positions
        player.transform.position = playerInitialPos;
        course.transform.position = courseInitialPos;
        int i = 0;
        foreach (Vector2 background in backgroundInitialPos)
        {
            movingPartsBackground[i].transform.position = background;
            i++;
        }
        //Restarts the coins
        foreach (GameObject coin in coins)
        {
            if (!coin.gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                coin.gameObject.GetComponent<SpriteRenderer>().enabled = !coin.gameObject.GetComponent<SpriteRenderer>().enabled;
                coin.gameObject.GetComponent<BoxCollider2D>().enabled = !coin.gameObject.GetComponent<BoxCollider2D>().enabled;
            }
          
        }

        score = 0;
        points = 0;
        pointsToCount = 0;
        atemptNumber++;
        atemptsText.SetText("Atempt: " + atemptNumber);
        gameStatus = true;
        course.GetComponent<AudioSource>().Play();
        return;
    }

    private void PointsCount(GameObject course)
    {
        if(course.transform.position.x < -16.67)
        {
            float pos = course.transform.position.x;
            float temp = (pos + 16.67f) / 6.6f;
            points = (int)temp*-1;
        }
        score = points + pointsToCount;
        scoreText.SetText("Score: " + score);
    }

    public void PauseButton()
    {
        pauseScreen.SetActive(true);
        gameStatus = false;
        paused = true;
    }

    public void UnPauseButton()
    {
        course.GetComponent<AudioSource>().UnPause();
        pauseScreen.SetActive(false);
        gameStatus = true;
        paused = false;
    }

    public void ClearAtempsGameManager()
    {
        
    }

    public void SaveReturnMenu()
    {
       setPlayerInfoToJSON();
        SceneManager.LoadSceneAsync("Title");
    }

    public void getPlayerInfoFromJSON()
    {

    }

    public void setPlayerInfoToJSON()
    {
        PlayerInfo playInfo = new PlayerInfo(level, score, atemptNumber);
        PlayerInfoManager playerInfoManager = GameObject.Find("LevelManager").GetComponent<PlayerInfoManager>();
        playerInfoManager.writeFile(playInfo);
    }
}

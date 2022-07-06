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
    private GameObject endScreen;
    private float vanishingpoint = -25f;
    private float initalPos = 25f;
    private float backgroundSpeed = 5f;
    public bool gameStatus;
    public bool gameResult;
    
    public int points;
    public int pointsToCount = 0;
    public bool paused = false;

    public PlayerInfo initialInfo;
    private int level;

    public int atemptNumber;
    public int score;

    private int highScore;


    private Vector2 playerInitialPos;
    private Vector2 courseInitialPos;
    private Vector2[] backgroundInitialPos;
    private GameObject[] coins;

    public GameObject[] courses;
    public AudioClip[] audios;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI atemptsText;

    public TextMeshProUGUI endScore;
    public TextMeshProUGUI endHScore;

    public GameObject camera;


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
                camera.GetComponent<AudioSource>().Pause();
            }
            else
            {
                if (!gameResult)
                {
                    camera.GetComponent<AudioSource>().Stop();
                    RestartOnLose();
                }
                else
                {
                    score = points + pointsToCount;
                    winAction(initialInfo.score[(level)]);
                }
            }
        }
    }
    void Initialize()
    {
        getPlayerInfoFromJSON();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        pauseScreen = GameObject.Find("Pause");
        endScreen = GameObject.Find("EndScreen");
        endScreen.SetActive(false);
        pauseScreen.SetActive(false);
        movingPartsBackground = GameObject.FindGameObjectsWithTag("MovingBackground");
        backgroundInitialPos = new Vector2[movingPartsBackground.Length];
        initialInfo = getPlayerInfoFromJSON();
        gameStatus = true;
        gameResult = false;
        if(atemptNumber == null || atemptNumber == 0)
        {
            atemptNumber = 1;
        }
        level = gameManager.chosenLevel;
        course = Instantiate(courses[level], new Vector3(-2.3f, -4f, 0f), new Quaternion(0, 0, 0, 0));
        camera.GetComponent<AudioSource>().clip = audios[level];
        camera.GetComponent<AudioSource>().Play();
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
        camera.GetComponent<AudioSource>().Play();
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
        camera.GetComponent<AudioSource>().UnPause();
        pauseScreen.SetActive(false);
        gameStatus = true;
        paused = false;
    }

    public void Save()
    {
       setPlayerInfoToJSON();
    }

    public PlayerInfo getPlayerInfoFromJSON()
    {
        PlayerInfoManager playerInfoManager = GameObject.Find("LevelManager").GetComponent<PlayerInfoManager>();
        initialInfo = playerInfoManager.readFile();
        return initialInfo;
    }

    public void setPlayerInfoToJSON()
    {
        PlayerInfo playInfo = new PlayerInfo(level, score, atemptNumber);
        PlayerInfoManager playerInfoManager = GameObject.Find("LevelManager").GetComponent<PlayerInfoManager>();
        playerInfoManager.writeFile(playInfo);
    }

    public void CallMenu(){
       Save();
       Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadSceneAsync("Title");
    }

    public void TryAgain(){
        SceneManager.LoadSceneAsync("Game");
    }

    public void winAction(int info){
       if( info> score){
            highScore = initialInfo.score[level];
       }else{
            highScore = score;
       }
        Save();
         camera.GetComponent<AudioSource>().Stop();
        endScreen.SetActive(true);
        endScore.text = "Score: " + score;
        endHScore.text = "High Score: " + highScore;
    }

    public void soundControl(){
        AudioSource[] sounds = GameObject.FindObjectsOfType<AudioSource>();
        if(camera.GetComponent<AudioSource>().volume > 0 ){
            foreach(AudioSource sound in sounds){
                sound.volume = 0;
            };
            
        }else{
             foreach(AudioSource sound in sounds){
                sound.volume = 1;
            };
        }
    }
}

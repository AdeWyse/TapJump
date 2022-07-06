using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
//Script responsable for managing the interactions on the level
public class LevelManager : MonoBehaviour
{

    private GameManager gameManager;
    private GameObject[] movingPartsBackground;// Variable for the background objects
    private GameObject player;
    public GameObject course;
    private GameObject pauseScreen;
    private GameObject endScreen;
    private float vanishingpoint = -25f;//Variable for the point on the screen where the backgroung is outside the camera view
    private float initalPos = 25f; //Initial position for the course spawnpoint
    private float backgroundSpeed = 5f;
    public bool gameStatus;//Variable controlling if the game is active or not
    public bool gameResult;//Variable controlling if the player won
    
    public int points; // points by distance
    public int pointsToCount = 0; //points by picking coins
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
    /*Is  checking the gameStatus, gameResult and paused variables to control if the game is active and apropriates reactions*/
    void Update()
    {   //Checks if the game is active, if the player has won and if the game is paused.
        //If is active and not won and not paused moves the obstacled and counts points
        //If paused calls the pause screen and stops all movemment from obstacles and background and stops sound
        //If not active and not won moves averything to start position, zeros all points adds a atempt and restarts sound
        //If not active and won calculates score and calls end screen
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

    /*Initializes the level by getting all needed references, 
    setting booleans and ints to apropriated velues, 
    spawning necessaries objects at appropriate positions and starting sound*/   
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
        level = gameManager.chosenLevel;// gets the level from the GameManager script
        course = Instantiate(courses[level], new Vector3(-2.3f, -4f, 0f), new Quaternion(0, 0, 0, 0));
        camera.GetComponent<AudioSource>().clip = audios[level];
        camera.GetComponent<AudioSource>().Play();
        course.SetActive(true);
        coins = GameObject.FindGameObjectsWithTag("Coin");

        GetInitialPos();
    }
    /*Moves the background objects to give the impression of a continuous line. 
    After one object leaves the field of view its moved to the end of the other object so it can loop*/
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
    /*Moves the course object towards the player. 
    When the end is reached changes the booleans and gives the won and not active status*/
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

    /*Gets the initial position of all objects in the scene so it can be reset later*/
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

    /*Moves averything to start position, zeros all points adds a atempt, restarts coins and restarts sound*/
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
    //Calculates the score and updates the screen text
    //score = points by obstacle passed + points by pickng coins
    private void PointsCount(GameObject course)
    {
        //calculares how many obstacled were passed by the distance between them
        if(course.transform.position.x < -16.67)
        {
            float pos = course.transform.position.x;
            float temp = (pos + 16.67f) / 6.6f;
            points = (int)temp*-1;
        }

        score = points + pointsToCount;
        scoreText.SetText("Score: " + score);
    }
    /*Activates the pause screen and changes the pause boolean*/
    public void PauseButton()
    {
        pauseScreen.SetActive(true);
        gameStatus = false;
        paused = true;
    }
    /*Deactivates the pause screen and changes the pause boolean*/
    public void UnPauseButton()
    {
        camera.GetComponent<AudioSource>().UnPause();
        pauseScreen.SetActive(false);
        gameStatus = true;
        paused = false;
    }
    /*Calls the function responsible for saving the game via PlayerInfoManager*/
    public void Save()
    {
       setPlayerInfoToJSON();
    }
    /*Gets player data via PlayerInfoManager*/
    public PlayerInfo getPlayerInfoFromJSON()
    {
        PlayerInfoManager playerInfoManager = GameObject.Find("LevelManager").GetComponent<PlayerInfoManager>();
        initialInfo = playerInfoManager.readFile();
        return initialInfo;
    }
    /*Saves playerData via PlayerInfoManager*/
    public void setPlayerInfoToJSON()
    {
        PlayerInfo playInfo = new PlayerInfo(level, score, atemptNumber);
        PlayerInfoManager playerInfoManager = GameObject.Find("LevelManager").GetComponent<PlayerInfoManager>();
        playerInfoManager.writeFile(playInfo);
    }
    /*Saves the game and calls the title scene making sure the GameManager is destroyed*/
    public void CallMenu(){
       Save();
       Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadSceneAsync("Title");
    }
    //Reloads the scene
    public void TryAgain(){
        SceneManager.LoadSceneAsync("Game");
    }
    /*Calls end screen and calculates values to be displayed*/
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
    /*Controls if there will be audio on the scene. It doens't stop the audio, it simply puts all volume to 0 or 1*/
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

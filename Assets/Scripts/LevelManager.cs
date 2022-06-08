using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject[] movingPartsBackground;
    private GameObject player;
    public GameObject course;
    private float vanishingpoint = -21f;
    private float initalPos = 25f;
    private float backgroundSpeed = 5f;
    public bool gameStatus;
    public bool gameResult;
    public int atemptNumber;
    public int points;
    public int pointsToCount = 0;
    public int score;

    private int level;

    private Vector2 playerInitialPos;
    private Vector2 courseInitialPos;
    private Vector2[] backgroundInitialPos;
    private GameObject[] coins;

    public GameObject[] courses;

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
        if (gameStatus && !gameResult)
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
            if (!gameResult)
            {
                RestartOnLose();
            }
            else
            {
                score = points + pointsToCount;
            }
        }
    }
    void Initialize()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        movingPartsBackground = GameObject.FindGameObjectsWithTag("MovingBackground");
        backgroundInitialPos = new Vector2[movingPartsBackground.Length];

        gameStatus = gameManager.gameStatus;
        gameResult = gameManager.gameResult;
        atemptNumber = gameManager.atemptNumber;
        level = gameManager.chosenLevel;
        course = Instantiate(courses[level], new Vector3(-2.3f, -4f, 0f), new Quaternion(0,0,0,0));
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
        if(course.transform.position.x <= -135.2f)
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
        
        player.transform.position = playerInitialPos;
        course.transform.position = courseInitialPos;
        int i = 0;
        foreach(Vector2 background in backgroundInitialPos)
        {
            movingPartsBackground[i].transform.position = background;
            i++;
        }
        foreach (GameObject coin in coins)
        {
            coin.SetActive(true);
        }

        score = 0;
        points = 0;
        pointsToCount = 0;
        atemptNumber++;
        atemptsText.SetText("Atempt: " + atemptNumber);
        gameStatus = true;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] movingPartsBackground;
    private GameObject player;
    private GameObject course;
    private float vanishingpoint = -21f;
    private float initalPos = 25f;
    private float backgroundSpeed = 5f;
    public bool gameStatus = true;
    public bool gameResult = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        movingPartsBackground = GameObject.FindGameObjectsWithTag("MovingBackground");
        course = GameObject.Find("Course1");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatus)
        {
            foreach (GameObject movingBackground in movingPartsBackground)
            {
                MoveBackground(movingBackground);
            }
            MoveObstacles(course);
        }
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
        if(course.transform.position.x <= -120f)
        {
            gameStatus = false;
            gameResult = true;
        }
    }
}

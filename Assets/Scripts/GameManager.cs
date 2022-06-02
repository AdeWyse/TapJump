using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] movingPartsBackground;
    private GameObject player;
    private GameObject[] obstacles;
    private float vanishingpoint = -21f;
    private float initalPos = 25f;
    private float backgroundSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        movingPartsBackground = GameObject.FindGameObjectsWithTag("MovingBackground");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject movingBackground in movingPartsBackground)
        {
            MoveBackground(movingBackground);
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
}

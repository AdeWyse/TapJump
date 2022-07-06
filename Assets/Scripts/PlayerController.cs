using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Script responsible for controlling the player movemment. 
The movable space is divided by a up side and a down side. 
This script gets the position of a touch on the screen and decides on witch side of the screen it was.
Then moves the player in the direction of highest or lowest position allowed*/
public class PlayerController : MonoBehaviour
{
    private LevelManager levelManager;
    private GameObject player;
    private Rigidbody2D playerRb;
    private int inputSimple;
    public Vector2 inputPos;
    public Vector2 posFin;
    public float speed = 30f;


    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveOnTouch();
    }
    /* Checks to see if there is any touch on the screen. If there is gets the position of the touch and decides on witch side of the screen it was.
    Then moves the player in the direction of highest or lowest position allowed*/
   void MoveOnTouch()
    {
        inputSimple = Input.touchCount;
        if (inputSimple > 0)
        {
            inputPos = Input.GetTouch(0).position;
            if (inputPos.y > 284f)
            {
                posFin = new Vector2(player.transform.position.x, 3.72f);
                player.transform.position = Vector2.MoveTowards(player.transform.position, posFin, speed * Time.deltaTime);
            }
            if (inputPos.y <= 284f)
            {
                posFin = new Vector2(player.transform.position.x, -3.72f);
                player.transform.position = Vector2.MoveTowards(player.transform.position, posFin, speed * Time.deltaTime);
            }
        }
        else
        {
            return;
        }
    }
  /*When the player collide it has two difent paths:
    1 - Its a obstacle and brings end game and restart
    2 - Its a coin and it adds 5 points*/
    void OnCollisionEnter2D(Collision2D collision)
    {
        /*If it is a obstacle it will chage a boolean that is controling the status of the game on the LevelManager. 
          When it turns false the game restart*/
        if (collision.gameObject.tag == "Obstacle")
        {
            levelManager.gameStatus = false;
        }

        /*If it is a coin the it will add 5 point to the atual point count on the LevelManager, will play the coin sound and 
        make the coin invisible and not collidable*/
        if (collision.gameObject.tag == "Coin")
        {
            levelManager.pointsToCount += 5;
            AudioSource audioSource = collision.gameObject.GetComponent<AudioSource>();
            audioSource.Play();
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = !collision.gameObject.GetComponent<SpriteRenderer>().enabled;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = !collision.gameObject.GetComponent<BoxCollider2D>().enabled;
        }
    }


}

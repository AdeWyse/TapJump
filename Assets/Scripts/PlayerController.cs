using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Obstacle")
        {
            levelManager.gameStatus = false;
        }


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

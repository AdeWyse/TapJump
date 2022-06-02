using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // playerRb = player.GetComponent<Rigidbody2D>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Grrr");

        if (collision.gameObject.tag == "Obstacle")
        {

            gameManager.gameStatus = false;
            gameManager.gameResult = false;
        }
    }

}

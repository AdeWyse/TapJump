using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRb;
    private int inputSimple;
    public Vector2 inputPos;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputSimple = Input.touchCount;
        if(inputSimple > 0)
        {
            inputPos = Input.GetTouch(0).position;
            Vector2 posInicial = new Vector2(player.transform.position.x, player.transform.position.y);
            player.transform.position = Vector2.MoveTowards(inputPos, player.transform.position, speed * Time.deltaTime);
            if(player.transform.position.y < -3.0f)
            {
                player.transform.position = new Vector2(player.transform.position.x, -3f);
            }

        }

    }
}

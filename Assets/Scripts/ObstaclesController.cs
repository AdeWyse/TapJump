using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    private GameObject obstacle;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        obstacle = this.gameObject;
        obstacle.SetActive(true);
        active = true;
        InvokeRepeating("Blink", 0.2f, Random.Range(0.3f, 0.9f));

    }

    private void Blink()
    {
        if (active)
        {
            obstacle.SetActive(false);
            active = false;
        }
        else
        {
            obstacle.SetActive(true);
            active = true;
        } 
    }

}

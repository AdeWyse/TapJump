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
        StartCoroutine("Blink");

    }

    private IEnumerator Blink()
    {
        while (true)
            yield return new WaitForSeconds(1);
        {
            if (active)
            {
                obstacle.SetActive(false);
                active = false;
            }
            else
            {
                Debug.Log("here2");
                obstacle.SetActive(true);
                active = true;
            }
        } 

    }

}

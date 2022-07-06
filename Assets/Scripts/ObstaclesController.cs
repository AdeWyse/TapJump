using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Script responsablie for the "Bliking" of some of the obstacles*/
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
        /*Will invoke the function responsible to making the obstacle "blink" at a radomized speed*/
        InvokeRepeating("Blink", 0.2f, Random.Range(0.3f, 0.9f));

    }
    /*Will make the obstable this script is atached "blink". That is set it active and active. 
    Together with the InvokeRepeating function it gives the effect that the object is blinking*/
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

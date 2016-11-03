using UnityEngine;
using System.Collections;

public class ElevatorButtonController : MonoBehaviour {

    public bool nearbyPlatformButton;



    void Start()
    {

    }



    void Update()
    {

    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearbyPlatformButton = true;
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearbyPlatformButton = false;
        }
    }
}
using UnityEngine;
using System.Collections;

public class DoorButtonController : MonoBehaviour
{

    public DoorController theDoorController;

    public bool nearbyDoorButton;



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
            nearbyDoorButton = true;
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearbyDoorButton = false;
        }
    }
}
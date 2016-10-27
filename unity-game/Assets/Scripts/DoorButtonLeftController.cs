using UnityEngine;
using System.Collections;

public class DoorButtonLeftController : MonoBehaviour {

    public DoorController theDoorController;

    public bool nearbyLeftButton;



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
            nearbyLeftButton = true;
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearbyLeftButton = false;
        }
    }
}
using UnityEngine;
using System.Collections;

public class DoorButtonRightController : MonoBehaviour {

    public DoorController theDoorController;

    public bool nearbyRightButton;



	void Start ()
    {
	
	}
	


	void Update ()
    {
	
	}



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearbyRightButton = true;
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearbyRightButton = false;
        }
    }
}
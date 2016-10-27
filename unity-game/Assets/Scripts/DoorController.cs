using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    public DoorButtonController theDoorButtonController;

    public bool doorOpened;
    public bool nearbyButton;

    public float doorSpeed;
    private float startTime;
    private float journeyLength;

    public GameObject door;

    [SerializeField]
    private Transform doorOpenedTransform;

    [SerializeField]
    private Transform doorClosedTransform;



    void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 8, true);
    }



    void Update()
    {
        nearbyButton = theDoorButtonController.GetComponent<DoorButtonController>().nearbyDoorButton;
        

        if (nearbyButton)
        {
            if (Input.GetButtonDown("Interact"))
            {
                doorOpened = !doorOpened;
            }
        }


        if (doorOpened)
        {
            float step = doorSpeed * Time.deltaTime;
            door.transform.position = Vector3.LerpUnclamped(doorOpenedTransform.position, doorOpenedTransform.transform.position, step);
        }


        if (!doorOpened)
        {
            float step = doorSpeed * Time.deltaTime;
            door.transform.position = Vector3.LerpUnclamped(doorClosedTransform.position, doorClosedTransform.transform.position, step);
        }
    }
}
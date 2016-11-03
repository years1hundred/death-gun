using UnityEngine;
using System.Collections;

public class ManualElevator : MonoBehaviour {

    public ElevatorButtonController theElevatorButtonController;

    public bool notMoving;
    public bool nearbyButton;
    public bool reachedDestination;

    public GameObject objectToMove;
    public GameObject theRightWall;
    public GameObject theLeftWall;
    public GameObject firstRightBarrier;
    public GameObject secondRightBarrier;
    public GameObject firstLeftBarrier;
    public GameObject secondLeftBarrier;

    public Transform startPoint;
    public Transform endPoint;

    public float moveSpeed;
    public float readyTimer;

    private Vector3 currentTarget;


    void Start()
    {
        currentTarget = endPoint.position;
        readyTimer = .5f;
        notMoving = true;
    }



    void Update()
    {
        nearbyButton = theElevatorButtonController.GetComponent<ElevatorButtonController>().nearbyPlatformButton;



        if (objectToMove.transform.position == endPoint.position)
        {
            if (readyTimer == 0f)
            {
                notMoving = true;
                reachedDestination = true;
            }            
            currentTarget = startPoint.position;
            theLeftWall.SetActive(false);
            firstLeftBarrier.SetActive(false);
        }
        else
        {
            theLeftWall.SetActive(true);
        }

        if (objectToMove.transform.position == startPoint.position)
        {
            if (readyTimer == 0f)
            {
                notMoving = true;
                reachedDestination = true;
            }
            currentTarget = endPoint.position;
            theRightWall.SetActive(false);
            firstRightBarrier.SetActive(false);
        }
        else
        {
            theRightWall.SetActive(true);
        }



        if (reachedDestination)
        {
            readyTimer = .5f;
            reachedDestination = false;
        }



        if (!notMoving)
        {
            //readyTimer -= Time.deltaTime;
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);
            readyTimer = 0f;
        }



        if (readyTimer == .5f)
        {
            if (Input.GetButtonDown("Interact"))
            {
                notMoving = false;
                firstRightBarrier.SetActive(true);
                firstLeftBarrier.SetActive(true);
                //objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);
            }
        }
    }
}

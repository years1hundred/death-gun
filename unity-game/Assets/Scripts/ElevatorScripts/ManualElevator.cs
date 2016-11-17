using UnityEngine;
using System.Collections;

public class ManualElevator : MonoBehaviour {

    public ElevatorButtonController theElevatorButtonController;

    public bool notMoving;
    public bool nearbyButton;
    public bool reachedDestination;
    public bool onStartPoint;
    public bool onEndPoint;

    public GameObject objectToMove;
    public GameObject theRightWall;
    public GameObject theLeftWall;
    public GameObject theStartLeftBarrier;
    public GameObject theStartRightBarrier;
    public GameObject theEndLeftBarrier;
    public GameObject theEndRightBarrier;
    public GameObject theStartPoint;
    public GameObject theEndPoint;

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

        theLeftWall.SetActive(false);
        theRightWall.SetActive(false);
        theStartLeftBarrier.SetActive(false);
        theStartRightBarrier.SetActive(false);
        theEndLeftBarrier.SetActive(false);
        theEndRightBarrier.SetActive(false);
    }



    void Update()
    {
        nearbyButton = theElevatorButtonController.GetComponent<ElevatorButtonController>().nearbyPlatformButton;



        if (objectToMove.transform.position == endPoint.position)
        {
            onStartPoint = false;
            onEndPoint = true;
            if (readyTimer == 0f)
            {
                notMoving = true;
                reachedDestination = true;
                theStartLeftBarrier.SetActive(false);
                theStartRightBarrier.SetActive(false);
                theEndLeftBarrier.SetActive(false);
                theEndRightBarrier.SetActive(false);
            }            
            currentTarget = startPoint.position;
            //firstLeftBarrier.SetActive(false);
            if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == true)
            {
                theLeftWall.SetActive(true);
            }
            else if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == false)
            {
                theLeftWall.SetActive(false);
            }
            if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == true)
            {
                theRightWall.SetActive(true);
            }
            else if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == false)
            {
                theRightWall.SetActive(false);
            }
        }

        if (objectToMove.transform.position == startPoint.position)
        {
            onEndPoint = false;
            onStartPoint = true;
            if (readyTimer == 0f)
            {
                notMoving = true;
                reachedDestination = true;
                theStartLeftBarrier.SetActive(false);
                theStartRightBarrier.SetActive(false);
                theEndLeftBarrier.SetActive(false);
                theEndRightBarrier.SetActive(false);
            }
            currentTarget = endPoint.position;
            //firstRightBarrier.SetActive(false);
            if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == true)
            {
                theLeftWall.SetActive(true);
            }
            else if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == false)
            {
                theLeftWall.SetActive(false);
            }
            if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == true)
            {
                theRightWall.SetActive(true);
            }
            else if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == false)
            {
                theRightWall.SetActive(false);
            }
        }



        if (onStartPoint)
        {
            if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == true)
            {
                theEndLeftBarrier.SetActive(true);
            }
            else if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == false)
            {
                theEndLeftBarrier.SetActive(false);
            }
            if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == true)
            {
                theEndRightBarrier.SetActive(true);
            }
            else if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == false)
            {
                theEndRightBarrier.SetActive(false);
            }
        }



        if (onEndPoint)
        {
            if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == true)
            {
                theStartLeftBarrier.SetActive(true);
            }
            else if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == false)
            {
                theStartLeftBarrier.SetActive(false);
            }
            if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == true)
            {
                theStartRightBarrier.SetActive(true);
            }
            else if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == false)
            {
                theStartRightBarrier.SetActive(false);
            }
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
            theLeftWall.SetActive(true);
            theRightWall.SetActive(true);
            if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == true)
            {
                theStartLeftBarrier.SetActive(true);
            }
            else if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == false)
            {
                theStartLeftBarrier.SetActive(false);
            }
            if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == true)
            {
                theStartRightBarrier.SetActive(true);
            }
            else if (theStartPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == false)
            {
                theStartRightBarrier.SetActive(false);
            }
            if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == true)
            {
                theEndLeftBarrier.SetActive(true);
            }
            else if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == false)
            {
                theEndLeftBarrier.SetActive(false);
            }
            if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == true)
            {
                theEndRightBarrier.SetActive(true);
            }
            else if (theEndPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == false)
            {
                theEndRightBarrier.SetActive(false);
            }
        }



        if (readyTimer == .5f)
        {
            if (Input.GetButtonDown("Interact"))
            {
                notMoving = false;
            }
        }
    }
}

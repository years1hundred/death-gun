using UnityEngine;
using System.Collections;

public class AutoElevator : MonoBehaviour
{
    public bool currentlyMoving;
    public bool reachedDestination;
    public bool onStartPoint;
    public bool onEndPoint;

    public GameObject elevatorObject;
    public GameObject theRightWall;
    public GameObject theLeftWall;
    public GameObject theStartLeftBarrier;
    public GameObject theStartRightBarrier;
    public GameObject theEndLeftBarrier;
    public GameObject theEndRightBarrier;
    public GameObject startPoint;
    public GameObject endPoint;

    public float moveSpeed;
    public float waitTime;
    public float waitCounter;
    
    private Vector3 currentTarget;


    void Awake()
    {
        currentTarget = endPoint.transform.position;

        waitCounter = waitTime;

        theLeftWall.SetActive(false);
        theRightWall.SetActive(false);
        theStartLeftBarrier.SetActive(false);
        theStartRightBarrier.SetActive(false);
        theEndLeftBarrier.SetActive(false);
        theEndRightBarrier.SetActive(false);
    }



    void Start()
    {

    }



    void Update()
    {     
        if (elevatorObject.transform.position == endPoint.transform.position)
        {
            onStartPoint = false;
            onEndPoint = true;
            if (waitCounter < 0f)
            {
                reachedDestination = true;
                theStartLeftBarrier.SetActive(false);
                theStartRightBarrier.SetActive(false);
                theEndLeftBarrier.SetActive(false);
                theEndRightBarrier.SetActive(false);
            }
            currentlyMoving = false;
            currentTarget = startPoint.transform.position;
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == true)
            {
                theLeftWall.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == false)
            {
                theLeftWall.SetActive(false);
            }
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == true)
            {
                theRightWall.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == false)
            {
                theRightWall.SetActive(false);
            }
        }

        if (elevatorObject.transform.position == startPoint.transform.position)
        {
            onEndPoint = false;
            onStartPoint = true;
            if (waitCounter < 0f)
            {
                reachedDestination = true;
                theStartLeftBarrier.SetActive(false);
                theStartRightBarrier.SetActive(false);
                theEndLeftBarrier.SetActive(false);
                theEndRightBarrier.SetActive(false);
            }
            currentlyMoving = false;
            currentTarget = endPoint.transform.position;
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == true)
            {
                theLeftWall.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == false)
            {
                theLeftWall.SetActive(false);
            }
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == true)
            {
                theRightWall.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == false)
            {
                theRightWall.SetActive(false);
            }
        }



        if (onStartPoint)
        {
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == true)
            {
                theEndLeftBarrier.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == false)
            {
                theEndLeftBarrier.SetActive(false);
            }
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == true)
            {
                theEndRightBarrier.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == false)
            {
                theEndRightBarrier.SetActive(false);
            }
        }



        if (onEndPoint)
        {
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == true)
            {
                theStartLeftBarrier.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == false)
            {
                theStartLeftBarrier.SetActive(false);
            }
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == true)
            {
                theStartRightBarrier.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == false)
            {
                theStartRightBarrier.SetActive(false);
            }
        }



        if (reachedDestination)
        {
            waitCounter = waitTime;
            reachedDestination = false;
        }



        if (!currentlyMoving)
        {
            waitCounter -= Time.fixedDeltaTime;
        }



        if (currentlyMoving)
        {
            theLeftWall.SetActive(true);
            theRightWall.SetActive(true);
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == true)
            {
                theStartLeftBarrier.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == false)
            {
                theStartLeftBarrier.SetActive(false);
            }
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == true)
            {
                theStartRightBarrier.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == false)
            {
                theStartRightBarrier.SetActive(false);
            }
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == true)
            {
                theEndLeftBarrier.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == false)
            {
                theEndLeftBarrier.SetActive(false);
            }
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == true)
            {
                theEndRightBarrier.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == false)
            {
                theEndRightBarrier.SetActive(false);
            }
        }



        if (waitCounter < 0f)
        {
            currentlyMoving = true;
            elevatorObject.transform.position = Vector3.MoveTowards(elevatorObject.transform.position, currentTarget, moveSpeed * Time.fixedDeltaTime);
        }
    }
}

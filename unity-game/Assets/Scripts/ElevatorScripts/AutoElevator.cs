using UnityEngine;
using System.Collections;

public class AutoElevator : MonoBehaviour
{
    public bool notMoving;
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
    public float waitTime;
    public float waitCounter;
    
    private Vector3 currentTarget;


    void Start()
    {
        currentTarget = endPoint.position;

        waitCounter = waitTime;

        theLeftWall.SetActive(false);
        theRightWall.SetActive(false);
        theStartLeftBarrier.SetActive(false);
        theStartRightBarrier.SetActive(false);
        theEndLeftBarrier.SetActive(false);
        theEndRightBarrier.SetActive(false);
    }



    void Update()
    {     
        if (objectToMove.transform.position == endPoint.position)
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
            notMoving = true;
            currentTarget = startPoint.position;
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
            if (waitCounter < 0f)
            {
                reachedDestination = true;
                theStartLeftBarrier.SetActive(false);
                theStartRightBarrier.SetActive(false);
                theEndLeftBarrier.SetActive(false);
                theEndRightBarrier.SetActive(false);
            }
            notMoving = true;
            currentTarget = endPoint.position;
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
            waitCounter = waitTime;
            reachedDestination = false;
        }



        if (notMoving)
        {
            waitCounter -= Time.deltaTime;
        }



        if (!notMoving)
        {
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



        if (waitCounter < 0f)
        {
            notMoving = false;
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);
        }
    }
}

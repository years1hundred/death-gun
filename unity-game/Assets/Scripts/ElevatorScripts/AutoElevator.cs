using UnityEngine;
using System.Collections;

public class AutoElevator : MonoBehaviour
{
    public bool currentlyMoving;
    public bool reachedDestination;
    public bool onStartPoint;
    public bool onEndPoint;

    public GameObject elevatorObject;
    public GameObject rightWall;
    public GameObject leftWall;
    public GameObject startLeftBarrier;
    public GameObject startRightBarrier;
    public GameObject endLeftBarrier;
    public GameObject endRightBarrier;
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

        leftWall.SetActive(false);
        rightWall.SetActive(false);
        startLeftBarrier.SetActive(false);
        startRightBarrier.SetActive(false);
        endLeftBarrier.SetActive(false);
        endRightBarrier.SetActive(false);
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
                startLeftBarrier.SetActive(false);
                startRightBarrier.SetActive(false);
                endLeftBarrier.SetActive(false);
                endRightBarrier.SetActive(false);
            }
            currentlyMoving = false;
            currentTarget = startPoint.transform.position;
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == true)
            {
                leftWall.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == false)
            {
                leftWall.SetActive(false);
            }
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == true)
            {
                rightWall.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == false)
            {
                rightWall.SetActive(false);
            }
        }

        if (elevatorObject.transform.position == startPoint.transform.position)
        {
            onEndPoint = false;
            onStartPoint = true;
            if (waitCounter < 0f)
            {
                reachedDestination = true;
                startLeftBarrier.SetActive(false);
                startRightBarrier.SetActive(false);
                endLeftBarrier.SetActive(false);
                endRightBarrier.SetActive(false);
            }
            currentlyMoving = false;
            currentTarget = endPoint.transform.position;
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == true)
            {
                leftWall.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().leftWall == false)
            {
                leftWall.SetActive(false);
            }
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == true)
            {
                rightWall.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().rightWall == false)
            {
                rightWall.SetActive(false);
            }
        }



        if (onStartPoint)
        {
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == true)
            {
                endLeftBarrier.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == false)
            {
                endLeftBarrier.SetActive(false);
            }
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == true)
            {
                endRightBarrier.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == false)
            {
                endRightBarrier.SetActive(false);
            }
        }



        if (onEndPoint)
        {
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == true)
            {
                startLeftBarrier.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == false)
            {
                startLeftBarrier.SetActive(false);
            }
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == true)
            {
                startRightBarrier.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == false)
            {
                startRightBarrier.SetActive(false);
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
            leftWall.SetActive(true);
            rightWall.SetActive(true);
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == true)
            {
                startLeftBarrier.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().startLeftBarrier == false)
            {
                startLeftBarrier.SetActive(false);
            }
            if (startPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == true)
            {
                startRightBarrier.SetActive(true);
            }
            else if (startPoint.gameObject.GetComponent<ElevatorBookends>().startRightBarrier == false)
            {
                startRightBarrier.SetActive(false);
            }
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == true)
            {
                endLeftBarrier.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().endLeftBarrier == false)
            {
                endLeftBarrier.SetActive(false);
            }
            if (endPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == true)
            {
                endRightBarrier.SetActive(true);
            }
            else if (endPoint.gameObject.GetComponent<ElevatorBookends>().endRightBarrier == false)
            {
                endRightBarrier.SetActive(false);
            }
        }



        if (waitCounter < 0f)
        {
            currentlyMoving = true;
            elevatorObject.transform.position = Vector3.MoveTowards(elevatorObject.transform.position, currentTarget, moveSpeed * Time.fixedDeltaTime);
        }
    }
}

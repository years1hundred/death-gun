using UnityEngine;
using System.Collections;

public class ManualElevator : MonoBehaviour {

    public ElevatorButtonController theElevatorButtonController;
    public PlayerController thePlayerControllerScript;

    public bool currentlyMoving;
    public bool nearbyButton;
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
    public float readyTimer;

    private Vector3 currentTarget;



    void Awake()
    {
        currentTarget = endPoint.transform.position;
        readyTimer = .5f;
        currentlyMoving = false;

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
        nearbyButton = theElevatorButtonController.GetComponent<ElevatorButtonController>().nearbyPlatformButton;
        

        if (elevatorObject.transform.position == endPoint.transform.position)
        {
            onStartPoint = false;
            onEndPoint = true;
            if (readyTimer == 0f)
            {
                currentlyMoving = false;
                reachedDestination = true;
                startLeftBarrier.SetActive(false);
                startRightBarrier.SetActive(false);
                endLeftBarrier.SetActive(false);
                endRightBarrier.SetActive(false);
            }            
            currentTarget = startPoint.transform.position;
            //firstLeftBarrier.SetActive(false);
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
            if (readyTimer == 0f)
            {
                currentlyMoving = false;
                reachedDestination = true;
                startLeftBarrier.SetActive(false);
                startRightBarrier.SetActive(false);
                endLeftBarrier.SetActive(false);
                endRightBarrier.SetActive(false);
            }
            currentTarget = endPoint.transform.position;
            //firstRightBarrier.SetActive(false);
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
            readyTimer = .5f;
            reachedDestination = false;
        }



        if (currentlyMoving)
        {
            //readyTimer -= Time.deltaTime;
            elevatorObject.transform.position = Vector3.MoveTowards(elevatorObject.transform.position, currentTarget, moveSpeed * Time.deltaTime);
            readyTimer = 0f;
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



        if (readyTimer == .5f)
        {
            if (nearbyButton)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    currentlyMoving = true;
                }
            }
        }
    }
}

﻿using UnityEngine;
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
    public GameObject theRightWall;
    public GameObject theLeftWall;
    public GameObject theStartLeftBarrier;
    public GameObject theStartRightBarrier;
    public GameObject theEndLeftBarrier;
    public GameObject theEndRightBarrier;
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
        nearbyButton = theElevatorButtonController.GetComponent<ElevatorButtonController>().nearbyPlatformButton;
        

        if (elevatorObject.transform.position == endPoint.transform.position)
        {
            onStartPoint = false;
            onEndPoint = true;
            if (readyTimer == 0f)
            {
                currentlyMoving = false;
                reachedDestination = true;
                theStartLeftBarrier.SetActive(false);
                theStartRightBarrier.SetActive(false);
                theEndLeftBarrier.SetActive(false);
                theEndRightBarrier.SetActive(false);
            }            
            currentTarget = startPoint.transform.position;
            //firstLeftBarrier.SetActive(false);
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
            if (readyTimer == 0f)
            {
                currentlyMoving = false;
                reachedDestination = true;
                theStartLeftBarrier.SetActive(false);
                theStartRightBarrier.SetActive(false);
                theEndLeftBarrier.SetActive(false);
                theEndRightBarrier.SetActive(false);
            }
            currentTarget = endPoint.transform.position;
            //firstRightBarrier.SetActive(false);
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
            readyTimer = .5f;
            reachedDestination = false;
        }



        if (currentlyMoving)
        {
            //readyTimer -= Time.deltaTime;
            elevatorObject.transform.position = Vector3.MoveTowards(elevatorObject.transform.position, currentTarget, moveSpeed * Time.deltaTime);
            readyTimer = 0f;
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

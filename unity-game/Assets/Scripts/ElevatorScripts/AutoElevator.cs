using UnityEngine;
using System.Collections;

public class AutoElevator : MonoBehaviour
{
    public bool notMoving;
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
    public float waitTime;
    public float waitCounter;
    
    private Vector3 currentTarget;


    void Start()
    {
        currentTarget = endPoint.position;

        waitCounter = waitTime;
    }



    void Update()
    {     
        if (objectToMove.transform.position == endPoint.position)
        {
            if (waitCounter < 0f)
            {
                reachedDestination = true;
            }
            notMoving = true;
            currentTarget = startPoint.position;
            theLeftWall.SetActive(false);
            firstLeftBarrier.SetActive(false);
            secondLeftBarrier.SetActive(false);
        }
        else
        {
            theLeftWall.SetActive(true);
        }

        if (objectToMove.transform.position == startPoint.position)
        {
            if (waitCounter < 0f)
            {
                reachedDestination = true;
            }
            notMoving = true;
            currentTarget = endPoint.position;
            theRightWall.SetActive(false);
            firstRightBarrier.SetActive(false);
            secondRightBarrier.SetActive(false);
        }
        else
        {
            theRightWall.SetActive(true);
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



        if (waitCounter < 0f)
        {
            notMoving = false;
            firstRightBarrier.SetActive(true);
            firstLeftBarrier.SetActive(true);
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);
        }
    }
}

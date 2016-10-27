using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject target;

    public float followAhead;
    public float cameraSmoothing;

    private Vector3 targetPosition;



	void Start ()
    {
	
	}
	


	void Update ()
    {
        //Establishes targetPosition
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);


        //This moves the target of the camera ahead of the player
        if (target.transform.localScale.x > 0f)
        {
            targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
        }
        else
        {
            targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
        }


        //transform.position = targetPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSmoothing * Time.deltaTime);
	}
}

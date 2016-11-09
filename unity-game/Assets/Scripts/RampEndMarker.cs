using UnityEngine;
using System.Collections;

public class RampEndMarker : MonoBehaviour {
	private GameObject player;

	public int end;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			player.GetComponent<PlayerController> ().endRamp();


		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			player.GetComponent<PlayerController> ().enterRamp();
			Debug.Log ("Test");

		}
	}

}

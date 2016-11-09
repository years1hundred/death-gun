using UnityEngine;
using System.Collections;

public class RampTopBuffer : MonoBehaviour {
	private GameObject player;

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
}

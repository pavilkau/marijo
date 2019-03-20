using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpoint : MonoBehaviour {

	private GameObject gameController;

	void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController");
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag=="Player")
		{
			gameController.GetComponent<GameController>().setCurrentCheckpoint(transform.position);
		}
	}

}

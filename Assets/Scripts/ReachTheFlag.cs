using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTheFlag : MonoBehaviour {

	private GameObject gameController;
	private Animator flagAnimator;

	void Start()
	{
		gameController = GameObject.FindWithTag("GameController");
		flagAnimator = GetComponent<Animator>();
	}
	
	
	
	void OnTriggerEnter2D(Collider2D other)
	{		

			flagAnimator.SetBool("win",true);
			
			gameController.GetComponent<GameController>().gameIsOver();
			GetComponent<AudioSource>().Play();
			
	}

	
	

}

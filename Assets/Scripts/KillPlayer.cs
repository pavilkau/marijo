using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

	public GameObject gameController;
	private PlayerController player;
	private bool gameOver;
	private bool playerInvincible;
	
	void Start()
	{
		gameController = GameObject.FindWithTag("GameController");
		//gameOver=false;
	}

			

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{			
			player = other.GetComponent<PlayerController>();
			if(!player.isInvincible())
			{
				if(player.isSuperMario()==true)
				{
					player.reducePower();
				}
				else
				{
					player.killPlayer();
				}	
			}					
		}
	}
}
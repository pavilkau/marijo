using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag=="Player" && other.gameObject.GetComponent<PlayerController>().isInvincible()==false)
		{
			Destroy(gameObject);
			other.GetComponent<PlayerController>().powerUpMario();	
			Score.addPoints(1000);		
		}
	}
}

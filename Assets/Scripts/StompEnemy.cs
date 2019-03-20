using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour {

	public float bounceOffHeight;
	private Rigidbody2D playerRB;
	private bool enemyDead;

	void Start()
	{
		playerRB = transform.parent.GetComponent<Rigidbody2D>();
 
	}
	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag=="Enemy" && !enemyDead)
		{
			
			other.GetComponent<BoxCollider2D>().isTrigger = false;
			enemyDead=true;			
			GetComponent<AudioSource>().Play();
			other.GetComponent<EnemyPatrol>().setMovementSpeed(0);
			other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;			
			other.GetComponent<Rigidbody2D>().isKinematic = true;
			other.GetComponent<Animator>().SetBool("goombaDown", true);
			other.transform.position = new Vector2(other.transform.position.x, 0.75f);
			playerRB.velocity = new Vector2(playerRB.velocity.x, bounceOffHeight);
			Score.addPoints(50);
			yield return new WaitForSeconds(0.5f);			
			Destroy(other.gameObject);
			enemyDead=false;
		}
	}
}

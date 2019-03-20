using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	public GameObject camera;
	private GameObject gameController;
	private Animator marioAnimator;	
	public Transform headbutt;
	public Transform headstomp;
	public Transform groundCheck;
	public LayerMask groundLayer;		
	public float groundCheckRadius;
	private bool isGrounded;

	public AudioSource powerUpSound;
	public AudioSource jumpSound;
	public AudioSource killPlayerAudio;

	public Transform mouth;
	public GameObject fireball;
	public float rateOfFire;
	private float timeStamp;

	public Vector2 maxPlayerPos;
	public Vector2 minPlayerPos;
	public float speed = 1.0f;
	public float jumpHeight = 0.5f;

	private float mSpeed;	
	private float scaleX = 1.0f;
	private float scaleY = 1.0f;

	
	public int health;
	public int respawnHeight;
	private bool invincible;
	private bool respawning;
	private  bool dead;

	private bool superMario;
	private bool flameMario;

	private Vector2 marioColliderSize;
	private Vector3 marioGroundCheckPosition;
	private Vector3 marioHeadStompScale;
	private Vector3 marioHeadStompPosition;
	private Vector3 marioHeadbuttPosition;
	public Vector2 superMarioColliderSize;
	public Vector3 superMarioGroundCheckPosition;
	public Vector3 superMarioHeadStompPosition;
	public Vector3 superMarioHeadStompScale;
	public Vector3 superMarioHeadbuttPosition;

 
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		marioAnimator = GetComponent<Animator>();
		gameController = GameObject.FindGameObjectWithTag("GameController");
		marioColliderSize = GetComponent<CapsuleCollider2D>().size;
		marioGroundCheckPosition = groundCheck.localPosition;
		marioHeadStompPosition = headstomp.localPosition;
		marioHeadStompScale = headstomp.localScale;
		marioHeadbuttPosition = headbutt.localPosition;

		superMario=false;
		flameMario=false;
		
		}  

	void FixedUpdate()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		marioAnimator.SetBool("isGrounded", isGrounded);
		transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, camera.transform.position.x - 11.5f, maxPlayerPos.x),
            Mathf.Clamp(transform.position.y, minPlayerPos.y, maxPlayerPos.y));		
	}
	
	void Update()
	{
		if(gameObject != null)
		{
			if (transform.position.y < respawnHeight && !dead) 
			{
				killPlayer();								
			}
			mSpeed = Input.GetAxis("Horizontal");
			marioAnimator.SetFloat("Speed", Mathf.Abs(mSpeed));

			if (Input.GetKeyDown(KeyCode.X))
			{			
				if (isGrounded)
				{
					rb.velocity = new Vector2(0, jumpHeight);
					jumpSound.Play();
					isGrounded = false;
				}
			}		
			marioAnimator.SetBool("isGrounded", isGrounded);

			if (Time.time >= timeStamp && Input.GetKeyDown(KeyCode.Z) && flameMario == true)
			{
				Instantiate(fireball, mouth.position, mouth.rotation);
				timeStamp = Time.time + rateOfFire;
			}

			if(invincible)
			{
				StartCoroutine(makePlayerInvincible());
			} 
			if (mSpeed > 0)
			{
				transform.localScale = new Vector2(scaleX, scaleY);
			}
			else if (mSpeed < 0)
			{
				transform.localScale = new Vector2(-scaleX, scaleY);
			}
			rb.velocity = new Vector2(mSpeed * speed, rb.velocity.y);
		}
	}						
		

	

	public bool isSuperMario()
	{
		return superMario;
	}


	public bool isFlameMario()
	{
		return flameMario;
	}

	public bool isRespawning()
	{
		return respawning;
	}



	public int getPlayerHealth()
	{
		return health;
	}

	public IEnumerator respawnPlayer()
    {
		respawning=true;
		yield return new WaitForSeconds(0.5f);
		rb.transform.position = gameController.GetComponent<GameController>().getCurrentCheckpoint();
		yield return new WaitForSeconds(0.4f);
		respawning = false;
		dead=false;
    }

	void gameLost()
	{
		Debug.Log("U lose noob");
	}

	public void setVelocity(Vector2 value)
	{
		rb.velocity = value;
	}

	public void powerUpMario()
	{
		powerUpSound.Play();
		if(isSuperMario())
		{
			flameMario=true;
			marioAnimator.SetLayerWeight(1,0f);
			marioAnimator.SetLayerWeight(2,1f);
			
		}			
		else
		{
			superMario=true;
			marioAnimator.SetLayerWeight(1,1f);
		}		
		
		GetComponent<CapsuleCollider2D>().size = superMarioColliderSize;
		groundCheck.localPosition = superMarioGroundCheckPosition;
		headstomp.localPosition = superMarioHeadStompPosition; 
		headstomp.localScale = superMarioHeadStompScale;
		headbutt.localPosition = superMarioHeadbuttPosition;
	}

	public void reducePower()
	{
		invincible=true;
		marioAnimator.SetLayerWeight(1,0f);
		marioAnimator.SetLayerWeight(2,0f);
		GetComponent<CapsuleCollider2D>().size = marioColliderSize;
		groundCheck.localPosition = marioGroundCheckPosition;
		headstomp.localPosition = marioHeadStompPosition; 
		headstomp.localScale = marioHeadStompScale;
		headbutt.localPosition = marioHeadbuttPosition;
		superMario=false;
		flameMario=false;
	}

	IEnumerator makePlayerInvincible()
	{	
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.5f);
		Physics2D.IgnoreLayerCollision(9,11,true);		
		yield return new WaitForSeconds(1.5f);
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
		Physics2D.IgnoreLayerCollision(9,11,false);
		invincible = false;
	}

	public bool isInvincible()
	{
		return invincible;
	}

	public void killPlayer()
	{
		
		dead=true;
		health--;
		reducePower();
		if(health<1 && gameObject != null)
			{
				gameController.GetComponent<GameController>().gameIsOver();
				killPlayerAudio.Play();
				gameController.GetComponent<AudioSource>().Stop();
				Destroy(gameObject);							
			}
		else
		{
			Debug.Log("Health: " + getPlayerHealth());
			StartCoroutine(respawnPlayer());
		}	
	}
}
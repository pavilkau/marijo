using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
    
    public bool moveLeft;
    public float movementSpeed;
    private Animator anim;
    public Transform edgeCheck;
    private bool onGround;
    public LayerMask ground;
    public float edgeCheckRadius;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }
    void Update()
    {
        if(moveLeft)
        {
            transform.localScale = new Vector3(1f,1f,1f);
            rb.velocity = new Vector3(-movementSpeed, rb.velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(-1f,1f,1f);
            rb.velocity = new Vector3(movementSpeed, rb.velocity.y);
        }

        onGround = Physics2D.OverlapCircle(edgeCheck.position, edgeCheckRadius, ground);
        if(!onGround)
        {
            moveLeft = !moveLeft;
        }
    }  

    public void setMovementSpeed(float value)
    {
        movementSpeed = value;
    }


    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Ground")
        || other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
        {
            moveLeft = !moveLeft; 
        }                   
    }

    void stopAnimator()
    {
        anim.enabled = false;
    }

    public void getBurnt()
    {
        Score.addPoints(100);
        stopAnimator();
        gameObject.GetComponent<BoxCollider2D>().enabled = false; 
        gameObject.GetComponent<SpriteRenderer>().flipY = true;
        rb.freezeRotation = false;
        rb.AddForce(transform.up * 1 , ForceMode2D.Impulse);        
        gameObject.GetComponent<CircleCollider2D>().enabled = false;   
    }

}
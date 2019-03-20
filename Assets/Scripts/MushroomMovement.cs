using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMovement : MonoBehaviour {
    
    public bool moveLeft;
    public float movementSpeed;
    private Animator anim;


    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.enabled=false;
        
    }
    void Update()
    {
        if(moveLeft)
        {
            rb.velocity = new Vector3(-movementSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector3(movementSpeed, rb.velocity.y);
        }

    }  

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Ground")
        || other.gameObject.tag == "Wall")
        {
            moveLeft = !moveLeft; 
        }                   
    }

}
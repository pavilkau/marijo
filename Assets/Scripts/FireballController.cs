using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

    public float speed;
    private PlayerController player;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        if(player.transform.localScale.x < 0)
        {
            speed = -speed;
        }       
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }
 
    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player" && other.tag != "Ground")
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            gameObject.GetComponent<Animator>().SetBool("hit",true);
            if(other.tag == "Enemy")
            {
               other.GetComponent<EnemyPatrol>().getBurnt();               
            }  
             
        }
        else if(other.tag == "Ground")
        {
            rb.AddForce(transform.up * 3 , ForceMode2D.Impulse);            
        }
        
    }

}
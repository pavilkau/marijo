using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHeadbutt : MonoBehaviour {
	
    private float posX;
    private float posY;
    public float bounceHeight;
    public float waitTime;
    private Transform player;
    public Transform destroyedBrick;
    public AudioSource destroyBrick;
    public AudioSource coin;
    public AudioSource mushroom;

    void Start()
    {
        player = transform.parent;
        
    }
   
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {    
        if(other.gameObject.tag == "CoinBlock" || other.gameObject.tag == "Bricks"  || other.gameObject.tag == "MushroomBlock" )
        {     
           if(other.gameObject.tag == "Bricks" && player.GetComponent<PlayerController>().isSuperMario() == true)
           {
               Destroy(other.gameObject);
               destroyBrick.Play();
               Instantiate(destroyedBrick, other.transform.position, other.transform.rotation);

           }
            else if( other.gameObject.tag == "Bricks" && other.gameObject != null || other.gameObject.GetComponent<Animator>().GetBool("blockDepleted") == false)
            {
                other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y + bounceHeight);
                yield return new WaitForSeconds (waitTime);
                other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y - bounceHeight);
            }
            
            if(other.gameObject.transform.childCount>0)
            {
                if(other.gameObject.tag == "CoinBlock")
                {
                    other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("blockHit",true);
                    other.gameObject.GetComponent<Animator>().SetBool("blockDepleted", true);
                    Score.addPoints(200);
                    coin.Play();
                    yield return new WaitForSeconds(0.2f);
                    Destroy(other.gameObject.transform.GetChild(0).gameObject);
                }
                else if(other.gameObject.tag == "MushroomBlock")
                {
                    if(player.GetComponent<PlayerController>().isSuperMario()==false)
                    {
                        other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        mushroom.Play();
                        other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("blockHit",true);
                        other.gameObject.GetComponent<Animator>().SetBool("blockDepleted", true);

                    }
                    else if(player.GetComponent<PlayerController>().isSuperMario()==true && 
                         other.gameObject.transform.childCount>1)
                    {
                        other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        mushroom.Play();
                        other.gameObject.transform.GetChild(1).GetComponent<Animator>().SetBool("blockHit",true);
                        other.gameObject.GetComponent<Animator>().SetBool("blockDepleted", true);
                    }              
                }
            }                  
        }   
    }
}
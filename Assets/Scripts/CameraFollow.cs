using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Vector2 velocity;

    public float smoothTimeX;
    private GameObject player;
    private GameObject gameController;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void FixedUpdate()
    {
        if(player!=null)
        {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        if(player.GetComponent<PlayerController>().isRespawning())
        {
            transform.position = gameController.GetComponent<GameController>().getCurrentCheckpoint();
        }
        else if(player.transform.position.x > transform.position.x && !player.GetComponent<PlayerController>().isRespawning())
        {
            transform.position = new Vector3(posX,transform.position.y, transform.position.z);
        }
        
        
        

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    

   public GameObject restartText;
   public GameObject gameOverText;
   public GameObject controlsText;

    private Vector3 currentCheckpoint;
    private bool restartOK;
    void Start()
    {        
        restartOK=false;
        StartCoroutine(showControls());

    }

    IEnumerator showControls()
    {
        //controlsText.SetActive(true);
        yield  return new WaitForSeconds(6f);
        controlsText.SetActive(false);
    }
    

    void Update()
    {
        if(restartOK)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    
    public void setCurrentCheckpoint(Vector3 value)
    {
        currentCheckpoint = value;
    }
    
    public Vector3 getCurrentCheckpoint()
    {
        return currentCheckpoint;
    }
    

    public void gameIsOver()
    {
        GetComponent<AudioSource>().Stop();
        restartText.SetActive(true);
        gameOverText.SetActive(true);  
        restartOK=true;
        
    }
}

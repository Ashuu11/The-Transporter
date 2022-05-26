using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamesound : MonoBehaviour
{
   
  public AudioSource audio1;
   public AudioSource audio2;
    
    bool detected;
    
    void Start()
    {     
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit,100))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                detected = true;
                Debug.Log("seen");
            }
                
            else
                detected = false;
        }
        
        if (detected==true && audio1.isPlaying==false && audio2.isPlaying==false)
            {               
                    audio1.Play();
                    audio2.Stop();
           
            }

        if (detected == false && audio2.isPlaying==false && audio1.isPlaying==false)
            {
                audio2.Play();
                audio1.Stop();                
            }
            
        }
        
        
    
}

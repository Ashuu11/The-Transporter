using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passwordbox : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    [SerializeField] GameObject passwordUI = null;
    bool showUI=false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        if(dist<5)
        {
            showUI = true;
        }
        if(dist>5)
        {
            showUI = false;
        }
        if(showUI==true)
        {
            passwordUI.SetActive(true);
           // Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (showUI ==false)
        {
            passwordUI.SetActive(false);
           // Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if(passwordverifer.opened==true)
        {
            passwordUI.SetActive(false);
           
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

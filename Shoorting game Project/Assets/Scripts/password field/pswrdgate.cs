using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pswrdgate : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    Transform player;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
       if(passwordverifer.canOpen==true)
        {
            anim.SetBool("open", true);
            anim.SetBool("close",false);
        }
       if(dist>10)
        {
            anim.SetBool("close", true);
            anim.SetBool("open", false);
            passwordverifer.canOpen = false;
        }

    }
}

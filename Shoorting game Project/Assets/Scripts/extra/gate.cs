using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
    Transform player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        if(dist<10)
        {
            anim.SetBool("open",true);
            anim.SetBool("close",false);
           
        }
        if(dist>10)
        {
            anim.SetBool("close", true);
            anim.SetBool("open",false);

        }
    }
}

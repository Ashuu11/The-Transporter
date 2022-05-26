using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moderngate : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    Animator anim;
    float dist;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.position);
        if(dist<10)
        {
            anim.SetBool("open", true);
            anim.SetBool("close", false);
            Debug.Log("gate can open");

        }
        if(dist>10)
        {
            anim.SetBool("open",false);
            anim.SetBool("close", true);
        }
    }
}

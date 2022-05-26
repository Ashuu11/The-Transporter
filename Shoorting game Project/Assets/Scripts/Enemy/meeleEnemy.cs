using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class meeleEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    float dist;
    
    NavMeshAgent agent;
    public static bool chase, attack1, attack2;
    bool seen = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.position);

        RaycastHit sight;
        var sightDirection = player.position - transform.position;
        if(Physics.Raycast(transform.position,sightDirection,out sight,20))
        {
            if (sight.transform == player)
            {
                // anim.SetBool("chase", true);
                seen = true;
                Debug.Log("player is seen");
            }
            else
                seen = false;
        }

        if(dist <20 && dist>5 && seen==true)
        {
            chase = true;
            agent.SetDestination(player.position);
            Debug.Log("distance is calculating");
           transform.LookAt(player);
        }
        if(dist <= 5f)
        {
            chase = false;
            agent.SetDestination(transform.position);
           StartCoroutine( combat());
            Debug.Log("player is approaching");
        }

        if(dist>5)
        {
            attack1 = false;
            attack2 = false;
            
        }
        if(dist>20||!seen)
        {
            chase = false;
        }
    }
    IEnumerator combat()
    {
        attack1 = true;
        yield return new WaitForSeconds(.5f);
        attack2 = true;
    }

    
}

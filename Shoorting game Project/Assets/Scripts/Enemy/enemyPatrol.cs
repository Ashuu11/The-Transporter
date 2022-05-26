using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class enemyPatrol : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    Animator anim;
    float dist;
    Transform player;
    public float followdistance = 20;
    public float AttackDistance = 10.0f;
    [Range(0.0f, 1.0f)]
    public float AttackProbability = 0.5f;

    [Range(0.0f, 1.0f)]
    public float HitAccuracy = 0.5f;

    public float DamagePoints = 2.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        // dist = Vector3.Distance(player.position , transform.position);
        bool shoot = false;
        bool follow = (dist < followdistance);
        if (dist > followdistance || enemySight.isVisible == false)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
        if (enemySight.isVisible == true && dist < followdistance)
            doFollow();
        if (dist<=AttackDistance && enemySight.isVisible == true)
        {
           
            float random = Random.Range(0.0f, 1.0f);
            if (random > (1.0f - AttackProbability) && dist < AttackDistance)
            {

                shoot = true;
                ShootEvent();

            }
        }
        if (enemySight.isVisible != true || dist > followdistance)
        {
            this.anim.SetBool("Run", false);
        }
        if (!follow || shoot)
        {
            this.agent.SetDestination(transform.position);

        }
    }
    void doFollow()
    {
        this.agent.SetDestination(player.transform.position);
        if (dist < followdistance && dist > AttackDistance)
        {
            this.anim.SetBool("Run", enemySight.isVisible);
            this.anim.SetBool("Shoot", false);
        }
    }
    public void ShootEvent()
    {
        if (dist < AttackDistance)
        {
            this.anim.SetBool("Shoot", true);
        }


        float random = Random.Range(0.0f, 1.0f);

        // The higher the accuracy is, the more likely the player will be hit
        bool isHit = random > 1.0f - HitAccuracy;

        if (isHit)
        {
            player.SendMessage("takeDamage", DamagePoints,
                SendMessageOptions.DontRequireReceiver);
            Debug.Log("bullet is fired");

        }
    }
}

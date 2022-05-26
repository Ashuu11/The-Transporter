using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemyBehaviour : MonoBehaviour
{
    private Animator _animator;

    NavMeshAgent _navMeshAgent=null;

    Transform Player;

    public float AttackDistance = 10.0f;

    public float FollowDistance = 20.0f;

    [Range(0.0f, 1.0f)]
    public float AttackProbability = 0.5f;

    [Range(0.0f, 1.0f)]
    public float HitAccuracy = 0.5f;

    public float DamagePoints = 2.0f;

    public AudioSource GunSound = null;
    public Transform[] points;
    private int destPoint = 0;
    float dist;
    public GameObject effect = null;
    public Transform gunpoint = null;
    void Awake()
    {      
       _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       // _navMeshAgent.autoBraking = false;
       // GotoNextPoint();

    }
    void Update()
    {
        if (_navMeshAgent.enabled)
        {
            dist = Vector3.Distance(Player.transform.position, this.transform.position);
            bool shoot = false;
            bool follow = (dist < FollowDistance);
            

            if ( enemySight.isVisible==true)
            {
                if (follow)
                {
                    doFollow();
                }
                float random = Random.Range(0.0f, 1.0f);
                if (random > (1.0f - AttackProbability) && dist < AttackDistance)
                {
                    
                    shoot = true;
                    ShootEvent();
                   
                }
              
            }
            if(enemySight.isVisible!=true || dist>FollowDistance)
            {
                this._animator.SetBool("Run",false);
            }

        

            if (!follow || shoot)
            {
                _navMeshAgent.SetDestination(transform.position);
               
            }

        }

       
    }
   
    void doFollow()
    {
        _navMeshAgent.SetDestination(Player.transform.position);
        if (dist < FollowDistance && dist > AttackDistance)
        {
           _animator.SetBool("Run", true);
           _animator.SetBool("Shoot",false);
        }
    }
    IEnumerator waitForNextShoot()
    {
        yield return new WaitForSeconds(.5f);
        ShootEvent();

    }
    public void ShootEvent()
    {
        Debug.Log("shoot is accessed");
        if (dist < AttackDistance)
        {
            _animator.SetBool("Shoot", true);
            _animator.SetBool("Run",false);
        }
        float random = Random.Range(0.0f, 1.0f);
        bool isHit = random > 1.0f - HitAccuracy;//accuracy

        if (isHit)
        {
            GunSound.Play();
            RaycastHit bullet;
            var rayDirection = Player.position - transform.position;

            Physics.Raycast(transform.position, rayDirection, out bullet, 20);
            if(bullet.transform==Player)
            {
                
               // transform.LookAt(Player);
                Player.SendMessage("takeDamage", DamagePoints, SendMessageOptions.DontRequireReceiver);
                // Debug.Log("bullet is fired"); 
            }

        }
    }
   

}

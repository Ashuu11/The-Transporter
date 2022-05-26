using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectGivesDamage : MonoBehaviour
{
    public Transform player;
    private float DamagePoints;
    private float Distance;

    [SerializeField] private float damageFactor = 0.5f;
    [SerializeField] private float AttackDistance = 10f;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
        Distance = Vector3.Distance(this.transform.position, player.position);
        if(Distance < AttackDistance)
        {
            GiveDamage();
        }

    }

    void GiveDamage()
    {
        DamagePoints = Time.deltaTime * damageFactor;
        player.SendMessage("takeDamage", DamagePoints, SendMessageOptions.DontRequireReceiver);
    }
    
}

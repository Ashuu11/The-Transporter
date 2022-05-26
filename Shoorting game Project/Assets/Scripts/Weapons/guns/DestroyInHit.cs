using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInHit : MonoBehaviour , IDamageable
{
    public void DealDamage(int damage )
    {
        if( damage != 0)
        Destroy(gameObject);
    }

    
}

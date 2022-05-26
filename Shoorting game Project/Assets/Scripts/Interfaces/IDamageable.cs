using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    void DealDamage(int damage); // When this script is referenced in addition to monobehaviour, that script can deal with damaging stuffs 
} 

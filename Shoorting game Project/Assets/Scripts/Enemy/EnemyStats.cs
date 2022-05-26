using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName = "Enemy/Stats")]
public class EnemyStats : ScriptableObject 
{
    public string enemyName; // this script just creates a menue to enter following values and referencing it to enemyHealth script
    public int maxHealth;

}

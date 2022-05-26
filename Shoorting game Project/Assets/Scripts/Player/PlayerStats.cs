using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Player/Stats")]
public class PlayerStats : ScriptableObject
{
    public string PlayerName; // this script just creates a menue to enter following values and referencing it to enemyHealth script
    public int maxHealth;
    public int maxStamina;

}

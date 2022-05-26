using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AmmunitionManager : MonoBehaviour
{
    public static AmmunitionManager instance;

    public AmmunitionUi ammunitionUi;
        
    private Dictionary<AmmunitionTypes, int> ammunitionCounts = new Dictionary<AmmunitionTypes, int>();
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this); //stops us from having duplicates
        }

    }

    private void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(AmmunitionTypes)).Length; i++)
        {
            ammunitionCounts.Add((AmmunitionTypes)i, 0);
        }
    }

    public void AddAmmunition(int value, AmmunitionTypes ammunitionType)
    {
        ammunitionCounts[ammunitionType] += value;
        ammunitionUi.UpdateAmmunitionCount(ammunitionCounts[ammunitionType]);
    }

    public int GetAmmunitionCount(AmmunitionTypes ammunitionType)
    {
        return ammunitionCounts[ammunitionType];
    }

    public bool ConsumeAmmunition(AmmunitionTypes ammunitionType)
    {
        if(ammunitionCounts[ammunitionType] > 0)
        {
            ammunitionCounts[ammunitionType]--;
            ammunitionUi.UpdateAmmunitionCount(ammunitionCounts[ammunitionType]);
            return true;
        } 
        else
        {
            return false;
        }
    }
        

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public static PlayerWeaponHandler instance;
    
    private Gun currentGun;
    private GameObject currentGunPrefab;
    

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

    public void PickUpGun(Gun gun)
    {
        if(currentGun != null)
        {
            Instantiate(currentGun.gunPickUp, transform.position + transform.forward, Quaternion.identity);
            Destroy(currentGunPrefab);
        }
        currentGun = gun;
        currentGunPrefab = Instantiate(gun.gameObject, transform);

        AmmunitionManager.instance.ammunitionUi.UpdateAmmunitionType(currentGun);
    }
}

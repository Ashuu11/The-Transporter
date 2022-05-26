using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour, ILootable
{
    [SerializeField] private Gun gun;
    public void OnStartLook()
    {
        Debug.Log($"Started looking at {gun.gunName}!");
    }

    public void OnInteract()
    {
        PlayerWeaponHandler.instance.PickUpGun(gun);
        Destroy(gameObject);
    }

    public void OnEndLook()
    {
        Debug.Log($"Stopped looking at {gun.gunName}!");
    }
        
}

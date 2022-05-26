using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public string gunName;
    public GameObject gunPickUp;

    [Header("Stats")]
    public AmmunitionTypes ammunitionType;
    public int minimumDamage;
    public int maximumDamage;
    public float maximumRange;
    public float fireRate;
    public GameObject muzzleFlashPoint;
    public GameObject muzzleFlash;

    protected float timeOfLastShot;

    private Transform cameraTransform;
    private GameObject muzzleFlashInstantiate;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    protected void Fire()
    {
        if(AmmunitionManager.instance.ConsumeAmmunition(ammunitionType))
        {
            RaycastHit whatIHit;
            muzzleFlashInstantiate = Instantiate(muzzleFlash, muzzleFlashPoint.transform.position, Quaternion.identity);
            Destroy(muzzleFlashInstantiate, 0.1f);
            if (Physics.Raycast(cameraTransform.position, cameraTransform.transform.forward, out whatIHit, Mathf.Infinity))
            {
                IDamageable damageable = whatIHit.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    float normalizedDistance = whatIHit.distance / maximumRange;
                    if (normalizedDistance <= 1)
                    {
                        damageable.DealDamage(Mathf.RoundToInt(Mathf.Lerp(maximumDamage, minimumDamage, normalizedDistance)));
                    }
                }
            }
        }
        
    }
}

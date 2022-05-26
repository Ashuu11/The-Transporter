using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRaycasting : MonoBehaviour
{
    [SerializeField] private float raycastDistance;

    private ILootable currentTarget;

    private void Update()
    {
        HandleRaycast();

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(currentTarget != null)
            {
                currentTarget.OnInteract();
            }
        }
        
    }

    private void HandleRaycast()
    {
        RaycastHit whatIHit;

        if(Physics.Raycast(transform.position, transform.forward, out whatIHit, raycastDistance))
        {
            ILootable lootable = whatIHit.collider.GetComponent<ILootable>();

            if(lootable != null)
            {
                if(lootable == currentTarget)
                {
                    return;
                }
                else if(currentTarget != null)
                {
                    currentTarget.OnEndLook();
                    currentTarget = lootable;
                    currentTarget.OnStartLook();
                }
                else
                {
                    currentTarget = lootable;
                    currentTarget.OnStartLook();
                }
            }
            else
            {
                if(currentTarget != null)
                {
                    currentTarget.OnEndLook();
                    currentTarget = null;
                }
            }
        }
        else
        {
            if (currentTarget != null)
            {
                currentTarget.OnEndLook();
                currentTarget = null;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUi : MonoBehaviour
{
    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer() // any ui item having this script will always look towards player 
    {
        transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward, playerCamera.transform.rotation * Vector3.up);
        
    }
}

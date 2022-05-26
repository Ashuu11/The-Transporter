using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SemiAutomatic : Gun
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - timeOfLastShot >= 1 / fireRate) //fire every 1/fireRate frames
            {
                Fire();
                timeOfLastShot = Time.time;
            }
        }
    }

} 

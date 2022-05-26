using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Automatic : Gun
{
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(Time.time - timeOfLastShot >= 1 / fireRate) //fire every 1/fireRate frames
            {
                Fire();                
                timeOfLastShot = Time.time;
                
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform player;
   public static bool isVisible;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
         RaycastHit hit;
        var rayDirection = player.position - transform.position;
        rayDirection.Normalize();
        if (Physics.Raycast(transform.position, rayDirection,out hit,20))
        {
            if (hit.transform == player)
            {
                isVisible = true;
               // Debug.Log("player is seen");
            }
            if (hit.transform != player)
            {
                isVisible = false;
              //  Debug.Log("player is not seen");
            }
          
        }
    }
}

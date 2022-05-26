using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pswrdDistributor : MonoBehaviour
{
    // Start is called before the first frame update
    pawrdGenerator generator;
    [SerializeField] Text[] containers=null;
   // [SerializeField] Renderer[] planes;
     int[] containersNotAvailable;
    int randomRoom;
    int length,finalRoom;

    void Start()
    {
         length = containers.Length;
       
        for(int i=0;i<=generator.pswrdlist.Length;i++)
        {
            random();
            containers[finalRoom].text=generator.pswrdlist[i].ToString("0");
            //planes[finalRoom].material = generator.materialToUse[i];  //remove comment and put comment to above statement to use material
            containersNotAvailable[i] = finalRoom;
        }
    }

   int random()
    {
        randomRoom = Random.Range(0, length);
        for(int i=0;i<=generator.pswrdlist.Length;i++)
        {
            if (containersNotAvailable[i] == randomRoom)
                random();
            if (containersNotAvailable[i] != randomRoom)
                finalRoom = randomRoom;
        }
        return finalRoom;  
    }
    void Update()
    {
        
    }
}

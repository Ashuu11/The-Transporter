using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawrdGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] pswrdlist=null;
   // public Material[] materials;
   // [HideInInspector]public  Material[] materialToUse;
    void Start()
    {
        //switch the comment to use material instead of text passwords
       for(int i=0;i<=pswrdlist.Length;i++)
        {
            pswrdlist[i] = Random.Range(1, 100);
          // materialToUse[i] = materials[Random.Range(0, materials.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(pswrdlist==null)
        {
            Debug.Log("Password is not generated");
        }
    }
}

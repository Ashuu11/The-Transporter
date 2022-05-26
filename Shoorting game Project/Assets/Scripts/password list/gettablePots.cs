using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gettablePots : MonoBehaviour
{
    int[] password= new int[4];
   public  static string password_final = "";
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<4;i++)
        {
            password[i] = Random.Range(0, 9);
            password_final +=password[i].ToString();
        }
        Debug.Log(password_final);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

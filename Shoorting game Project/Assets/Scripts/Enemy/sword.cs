using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    // Start is called before the first frame update
    Transform Player;
    [SerializeField] float DamagePoints;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Player.SendMessage("takeDamage", DamagePoints,
            SendMessageOptions.DontRequireReceiver);
            Debug.Log("sword is hit");


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meeleanimController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("chase", meeleEnemy.chase);
        anim.SetBool("attack1", meeleEnemy.attack1);
        anim.SetBool("attack2", meeleEnemy.attack2);
      //  if(meeleEnemy.chase==false && meeleEnemy.attack1==false && meeleEnemy.attack2==false)
         //   anim.SetBool("idle", true);

    }
}

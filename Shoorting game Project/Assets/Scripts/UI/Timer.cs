using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float mTimer = 10;
    float sTimer = 60;
    float counter;
    public Text text;
    void Start()
    {
        mTimer = mTimer-1;
        sTimer = 60;
    }

    // Update is called once per frame
    void Update()
    {
        sTimer -= Time.deltaTime;
        if(sTimer<=0)
        {
           
            sTimer = 60;
            mTimer = mTimer - 1;
        }
        if(mTimer>=0)
        text.text =""+mTimer+":"+sTimer.ToString("0");
    }
}

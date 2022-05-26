using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anyKeyTC : MonoBehaviour
{
    float timer;
   [SerializeField] GameObject text;
    [SerializeField] GameObject menu = null;
    [SerializeField] GameObject bgWallpaper = null;
    [SerializeField] GameObject currentbg = null;
    bool isClick = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        isClick = false;
        menu.SetActive(false);
        bgWallpaper.SetActive(false);
        currentbg.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5 && isClick == false)
        {
            text.SetActive(true);
        }
        if (Input.anyKey && isClick == false)
        {
            text.SetActive(false);
            isClick = true;
            menu.SetActive(true);
           // bgWallpaper.SetActive(true);
            currentbg.SetActive(false);
        }
    }
}

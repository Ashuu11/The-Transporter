using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickering : MonoBehaviour
{
    public bool isFlickering = false;
    public float Timedelay;

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(Flickering());
        }

    }
    IEnumerator Flickering()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        Timedelay = Random.Range(3f, 3f);
        yield return new WaitForSeconds(Timedelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        Timedelay = Random.Range(3f, 3f);
        isFlickering = false;

    }
}

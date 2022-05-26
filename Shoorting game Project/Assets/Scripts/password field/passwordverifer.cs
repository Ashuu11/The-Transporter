using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class passwordverifer : MonoBehaviour
{

	[SerializeField]
	Text codeText;
 	string codeTextValue = "";
	//public static bool erase = false;
	public static bool canOpen = false;
    public static bool opened = false;

    // Update is called once per frame
    void Update()
	{
		codeText.text = codeTextValue;

		if (codeTextValue ==gettablePots.password_final)
		{
			canOpen = true;
			Debug.Log("password matched");
            canOpen = true;
            opened = true;
           
		}

		if (codeTextValue.Length >= 4)
			codeTextValue = "";
	
	}

	public void AddDigit(string digit)
	{
		if (codeTextValue.Length <= 4)
		{
			codeTextValue += digit;
		}
	}
    public void reset()
    {
        codeTextValue = "";

    }
    public void button0()
    {
      AddDigit("0");
        Debug.Log("accessed 0");
    }
    public void button1()
    {
       AddDigit("1");
    }
    public void button2()
    {
       AddDigit("2");
    }
    public void button3()
    {
      AddDigit("3");
    }
    public void button4()
    {
        AddDigit("4");
    }
    public void button5()
    {
       AddDigit("5");
    }
    public void button6()
    {
        AddDigit("6");
    }
    public void button7()
    {
        AddDigit("7");
    }
    public void button8()
    {
        AddDigit("8");
    }
    public void button9()
    {
       AddDigit("9");
    }
}

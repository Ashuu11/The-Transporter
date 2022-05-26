using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    // Start is called before the first frame update
    int indexNumber;
   public void startGame()
    {
        SceneManager.LoadScene("maze");
    }
    public void tutorial()
    {
        SceneManager.LoadScene("tutorial");
    }
    public void exit()
    {
        Application.Quit();
    }
}

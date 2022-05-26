using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausecanvas = null;
    void Start()
    {
        Time.timeScale = 1; 
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.Escape))
        {
            pausecanvas.SetActive(true);
            Time.timeScale=0;
        }

    }
    public void resume()
    {
        pausecanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("start scene");
        Time.timeScale = 1;
    }
    public void exit()
    {
        Application.Quit();
    }

}

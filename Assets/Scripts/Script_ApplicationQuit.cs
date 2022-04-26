using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_ApplicationQuit : MonoBehaviour
{

    public bool win;

    private void Start()
    {
        //DontDestroyOnLoad(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (win)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("Appy Quit");
                Application.Quit();
            }

        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Title()
    {
        SceneManager.LoadScene(0);
    }

    public void Win()
    {
        SceneManager.LoadScene(2);
    }

}

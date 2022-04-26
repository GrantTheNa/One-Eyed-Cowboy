using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_ApplicationQuit : MonoBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Appy Quit");
            Application.Quit();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

}

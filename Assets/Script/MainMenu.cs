using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ClickToStart()
    {
        Debug.Log("Start Game!");
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickToQuit()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    public void ClickToCredits()
    {
        Debug.Log("Credit!");
        SceneManager.LoadScene("CreditScene");
    }

    public void ClickToBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

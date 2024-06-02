using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
   public void play()
    {
       
        SceneManager.LoadScene("IntroCutScene");
    }

    public void About()
    {
        SceneManager.LoadScene("About");
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void BackToCredit1()
    {
        SceneManager.LoadScene("Credits1");
    }
    public void NextToCredit2()
    {
        SceneManager.LoadScene("Credits2");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits1");
    }
    public void instructions()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void Exit()
    {
        Application.Quit();
    }
}

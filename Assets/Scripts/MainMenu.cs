using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Loads maze scene when Play button is selected
    public void PlayMaze()
    {
        SceneManager.LoadScene("maze");
    }

    // Quits application when Quit button is selected
    public void QuitMaze()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

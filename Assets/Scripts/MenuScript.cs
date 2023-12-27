using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject operationPanel;
    public void gameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}

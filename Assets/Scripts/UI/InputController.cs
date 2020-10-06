using Assets.Scripts.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.isGameOver = false;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ScoresMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitMenu()
    {
        Application.Quit();
    }
}

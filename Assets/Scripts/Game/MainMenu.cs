using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGameButton()
    {
        GameData.Data.NewGame();

        SceneManager.LoadScene("DataScene");
    }

    public void HelpButton()
    {
        SceneManager.LoadScene("HelpScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            NewGameButton();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            ExitButton();
        }

        if (Input.GetKey(KeyCode.Question) || Input.GetKey(KeyCode.Space))
        {
            ExitButton();
        }
    }
}

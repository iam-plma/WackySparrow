using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static void GoToMenu(MenuNames name)
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        switch (name)
        {
            case MenuNames.Main:

                // go to MainMenu scene
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuNames.Pause:

                // instantiate prefab
                Object.Instantiate(Resources.Load("PauseMenu"));
                break;
            case MenuNames.GameOver:

                Object.Instantiate(Resources.Load("GameOverMenu"));
                break;
            case MenuNames.Game:

                SceneManager.LoadScene("Gameplay");
                break;
            case MenuNames.Difficulty:

                SceneManager.LoadScene("DifficultyMenu");
                break;
        }
    }

    public void HandlePlayButtonOnClickEvent()
    {
        
        GoToMenu(MenuNames.Difficulty);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Application.Quit();
    }

    public void HandleHelpButtonOnClickEvent()
    {
        
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("HelpMenu");
    }

    public void HandleMainMenuButtonOnClickEvent()
    {
        
        GoToMenu(MenuNames.Main);
    }

    public void HandleEasyButtonOnClickEvent()
    {
        
        ChooseDifficulty.Difficulty = Difficulties.Easy;
        GoToMenu(MenuNames.Game);
    }

    public void HandleMediumButtonOnClickEvent()
    {
        
        ChooseDifficulty.Difficulty = Difficulties.Medium;
        GoToMenu(MenuNames.Game);
    }

    public void HandleHardButtonOnClickEvent()
    {
        
        ChooseDifficulty.Difficulty = Difficulties.Hard;
        GoToMenu(MenuNames.Game);
    }
}

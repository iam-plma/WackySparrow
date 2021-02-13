using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    void Start()
    {
        // pause the game when added to the scene
        Time.timeScale = 0;
    }

    /// <summary>
    /// Handles the on click event from the Resume button
    /// </summary>
    public void HandleResumeButtonOnClickEvent()
    {
        
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles the on click event from the Quit button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        //AudioManager.Play(AudioClipName.Buttons);
        Destroy(gameObject);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        MenuManager.GoToMenu(MenuNames.Main);
    }
}

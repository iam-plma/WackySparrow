using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    static Text finalScore;
    int score;

    void Start()
    {
        finalScore = GameObject.FindGameObjectWithTag("finalScore").GetComponent<Text>();
        score = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().getScore();
        finalScore.text = "SCORE: " + score;
        // pause the game when added to the scene
        Time.timeScale = 0;

        FindObjectOfType<AudioManager>().Play("GameOver");
    }

    public void HandleRestartButtonOnClickEvent()
    {
        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuNames.Difficulty);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuNames.Main);
    }
}

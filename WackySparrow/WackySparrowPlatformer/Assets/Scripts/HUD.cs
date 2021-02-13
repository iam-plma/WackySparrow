using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private int lives = 3;
    private int score = 0;

    private Text livesText;
    private Text gameOverText;
    private Text scoreText;

    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        livesText = GameObject.FindGameObjectWithTag("Lives").GetComponent<Text>();
        gameOverText = GameObject.FindGameObjectWithTag("GameOver").GetComponent<Text>();
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();

        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHP()
    {
        FindObjectOfType<AudioManager>().Play("PlayerHit");
        lives--;
        if(lives > 0)
        {
            livesText.text = "Lives: " + lives;
            Respawn();
        }
        else
        {
            livesText.text = "Lives: " + lives;
            MenuManager.GoToMenu(MenuNames.GameOver);
        }
        
    }

    public void AddHP()
    {
        lives++;
        livesText.text = "Lives: " + lives;
    }

    private void Respawn()
    {
        GameObject newPlayer = Instantiate(player);
        newPlayer.transform.position = new Vector3(-6.6f, 4, newPlayer.transform.position.z);
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public int getScore()
    {
        return score;
    }
}

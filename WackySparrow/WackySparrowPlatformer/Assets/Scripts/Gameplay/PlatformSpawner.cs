using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject platform;

    [SerializeField]
    GameObject crow;

    private Vector2 screenBounds;
    private float halfPlatformWidth;
    private float halfPlatformHeight;
    private float previousPlatformPosition = 0;
    private float crowHeight;

    Timer spawnTimer;

    private void Awake()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        switch (ChooseDifficulty.Difficulty)
        {
            case Difficulties.Easy:
                spawnTimer.Duration = 2.6f;
                break;
            case Difficulties.Medium:
                spawnTimer.Duration = 1.7f;
                break;
            case Difficulties.Hard:
                spawnTimer.Duration = 1.1f;
                break;
        }
        spawnTimer.AddTimerFinishedListener(HandleSpawningTimerFinished);
    }

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        crowHeight = crow.gameObject.GetComponent<BoxCollider2D>().size.y;

        previousPlatformPosition = -1f;

        GameObject temp = Instantiate(platform);

        halfPlatformWidth = temp.GetComponent<BoxCollider2D>().size.x / 2;
        halfPlatformHeight = temp.GetComponent<BoxCollider2D>().size.y / 2;


        temp.transform.position = new Vector3(screenBounds.x + 3.6f,
            UnityEngine.Random.Range(previousPlatformPosition - 1.5f, previousPlatformPosition + 1.5f), 0);


        float enemySpawnChance = UnityEngine.Random.Range(0, 5f);

        if (ChooseDifficulty.Difficulty == Difficulties.Easy)
        {
            if (enemySpawnChance <= 1f)
            {
                gameObject.GetComponent<EnemySpawner>().SpawnEnemy(3f, 0.5f, temp.transform.position);
            }
        }
        else if (ChooseDifficulty.Difficulty == Difficulties.Medium)
        {
            if (enemySpawnChance <= 2.5f)
            {
                gameObject.GetComponent<EnemySpawner>().SpawnEnemy(3f, 0.5f, temp.transform.position);
            }
        }
        else if (ChooseDifficulty.Difficulty == Difficulties.Hard)
        {
            if (enemySpawnChance <= 4.5f)
            {
                gameObject.GetComponent<EnemySpawner>().SpawnEnemy(3f, 0.5f, temp.transform.position);
            }
        }

        spawnTimer.Run();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.GoToMenu(MenuNames.Pause);
        }
    }

    private void HandleSpawningTimerFinished()
    {
        GameObject temp = Instantiate(platform);

        halfPlatformWidth = temp.GetComponent<BoxCollider2D>().size.x / 2;
        halfPlatformHeight = temp.GetComponent<BoxCollider2D>().size.y / 2;
        

        temp.transform.position = new Vector3(screenBounds.x + 3.6f, 
            UnityEngine.Random.Range(previousPlatformPosition - 1.5f, previousPlatformPosition + 1.5f), 0);

        if (temp.transform.position.y > screenBounds.y - crowHeight*2
            || temp.transform.position.y < -screenBounds.y)
        {
            temp.transform.position = new Vector3(screenBounds.x + 3.6f,
            previousPlatformPosition, 0);
        }

        previousPlatformPosition = temp.gameObject.transform.position.y;

        float enemySpawnChance = UnityEngine.Random.Range(0, 5f);

        if(ChooseDifficulty.Difficulty == Difficulties.Easy)
        {
            if (enemySpawnChance <= 1.3f)
            {
                gameObject.GetComponent<EnemySpawner>().SpawnEnemy(3.2f, 0.5f, temp.transform.position);
            }
        }
        else if (ChooseDifficulty.Difficulty == Difficulties.Medium)
        {
            if (enemySpawnChance <= 2.5f)
            {
                gameObject.GetComponent<EnemySpawner>().SpawnEnemy(3.2f, 0.5f, temp.transform.position);
            }
        }
        else if (ChooseDifficulty.Difficulty == Difficulties.Hard)
        {
            if (enemySpawnChance <= 4.1f)
            {
                gameObject.GetComponent<EnemySpawner>().SpawnEnemy(3.2f, 0.5f, temp.transform.position);
            }
        }

        //Debug.Log(enemySpawnChance);

        spawnTimer.Run();
    }
}

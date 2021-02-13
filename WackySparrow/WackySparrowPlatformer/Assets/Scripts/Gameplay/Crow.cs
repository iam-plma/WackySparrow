using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    Rigidbody2D rb2d;

    private float movementSpeed;

    [SerializeField]
    GameObject prefabBullet;
    [SerializeField]
    GameObject prefabWormBuff;
    [SerializeField]
    GameObject prefabBootsBuff;
    [SerializeField]
    GameObject prefabSeedsBuff;

    Vector2 thrustDirection = Vector2.left;
    Timer bulletSpawnTimer;

    Transform buffRespawn;

    private void Awake()
    {
        bulletSpawnTimer = gameObject.AddComponent<Timer>();
        bulletSpawnTimer.Duration = 1.5f;
        bulletSpawnTimer.AddTimerFinishedListener(HandleSpawningTimerFinished);
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (ChooseDifficulty.Difficulty)
        {
            case Difficulties.Easy:
                movementSpeed = 3f;
                break;
            case Difficulties.Medium:
                movementSpeed = 5.5f;
                break;
            case Difficulties.Hard:
                movementSpeed = 8f;
                break;
        }
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(-movementSpeed, rb2d.velocity.y);
        bulletSpawnTimer.Run();
        HandleSpawningTimerFinished();
    }

    // Update is called once per frame
    void Update()
    {
        buffRespawn = transform;
    }

    private void OnBecameInvisible()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");

        int buffProbability = Random.Range(1, 3);
        int chooseBuff = Random.Range(1, 4);
        if(buffProbability == 1)    
        {
            if(chooseBuff == 1)
            {
                GameObject temp = Instantiate(prefabWormBuff);
                
                temp.transform.position = new Vector3(transform.position.x + 2f, transform.position.y+3.5f, transform.position.z);
            }
            else if( chooseBuff == 2)
            {
                GameObject temp = Instantiate(prefabBootsBuff);
                temp.transform.position = new Vector3(transform.position.x + 2f, transform.position.y + 3.5f, transform.position.z);
            }
            else if(chooseBuff == 3)
            {
                GameObject temp = Instantiate(prefabSeedsBuff);
                temp.transform.position = new Vector3(transform.position.x + 2f, transform.position.y + 3.5f, transform.position.z);
            }
        }


        Destroy(gameObject);
    }

    private void HandleSpawningTimerFinished()
    {
        FindObjectOfType<AudioManager>().Play("EnemyFire");
        GameObject temp = Instantiate(prefabBullet, new Vector3(gameObject.transform.position.x, 
            gameObject.transform.position.y + 0.84f, gameObject.transform.position.z), Quaternion.identity);
        temp.GetComponent<Bullet>().ApplyForce(thrustDirection);
        bulletSpawnTimer.Run();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody2D rb2d;

    private float movementSpeed;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

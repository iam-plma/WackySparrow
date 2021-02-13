using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    

    private float bulletSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyForce(Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * direction, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Crow")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }else if (collision.gameObject.tag == "Player") 
        {
            if (!collision.gameObject.GetComponent<Player>().isInvulnerable)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }else if (collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    BoxCollider2D boxCollider2D;

    [SerializeField]
    LayerMask platformsLayerMask;

    [SerializeField]
    LayerMask enemyLayerMask;

    float playerMovementSpeed = 15f;
    float jumpForce = 30f;

    private bool facingRight = true;
    public bool isInvulnerable = true;
    private bool shooting = false;
    private bool speedUp = false;
    private bool isGrounded;

    Timer invulnerableTimer;
    Timer shootTimer;
    Timer speedUpTimer;

    [SerializeField]
    GameObject prefabBullet;

    [SerializeField]
    Sprite sparrowStaying;

    [SerializeField]
    Sprite sparrowSitting;

    private void Awake()
    {
        invulnerableTimer = gameObject.AddComponent<Timer>();
        invulnerableTimer.Duration = 2f;
        invulnerableTimer.AddTimerFinishedListener(breakInvulnerable);

        shootTimer = gameObject.AddComponent<Timer>();
        shootTimer.Duration = 3f;
        shootTimer.AddTimerFinishedListener(breakShooting);

        speedUpTimer = gameObject.AddComponent<Timer>();
        speedUpTimer.Duration = 2f;
        speedUpTimer.AddTimerFinishedListener(breakSpeedUp);

        Instantiate(Resources.Load("InvulnerableSliderTimer"));
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        
        invulnerableTimer.Run();
        isGrounded = false;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = IsGrounded();

        if (((isGrounded && Input.GetKey(KeyCode.Space)) && !IsSitting()) || ((isGrounded && Input.GetKey(KeyCode.W)) && !IsSitting()))
        {
            Debug.Log(isGrounded);
            Vector2 jumpVector = Vector2.up * jumpForce;
            rb2d.velocity = jumpVector;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && shooting || Input.GetKeyDown(KeyCode.Mouse0) && shooting)
        {
            GameObject temp = Instantiate(prefabBullet, new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y + 0.84f, gameObject.transform.position.z), Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("PlayerFire");
            if (facingRight)
            {
                temp.GetComponent<Bullet>().ApplyForce(Vector2.right);
            }
           else
           {
               temp.GetComponent<Bullet>().ApplyForce(Vector2.left);
           }
            
        }
    }

    private void breakInvulnerable()
    {
        isInvulnerable = false;
    }

    private void breakShooting()
    {
        shooting = false;
    }

    private void breakSpeedUp()
    {
        speedUp = false;
    }

    private void FixedUpdate()
    {
        

        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<SpriteRenderer>().sprite = sparrowSitting;
            GetComponent<BoxCollider2D>().size = new Vector2(1.352173f, 1.215264f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0.01698947f, -0.03664148f); 
            HandleFacing();
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sparrowStaying;
            GetComponent<BoxCollider2D>().size = new Vector2(1.166825f, 2.726758f);
            GetComponent<BoxCollider2D>().offset = new Vector2(-0.01390195f, 0.02844048f);
        }

        if (!IsSitting())
        {
            HandleMovement();
        }
        
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    private bool IsAboveEnemy()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, enemyLayerMask);
        return raycastHit2d.collider != null;
    }

    private bool IsSitting()
    {
        if(GetComponent<SpriteRenderer>().sprite == sparrowSitting)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (speedUp)
            {
                rb2d.velocity = new Vector2(-playerMovementSpeed*2, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2(-playerMovementSpeed, rb2d.velocity.y);
            }
            
            if (facingRight)
            {
                Flip();
                facingRight = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (speedUp)
                {
                    rb2d.velocity = new Vector2(playerMovementSpeed*2, rb2d.velocity.y);
                }
                else
                {
                    rb2d.velocity = new Vector2(playerMovementSpeed, rb2d.velocity.y);
                }
                
                if (!facingRight)
                {
                    Flip();
                    facingRight = true;
                }
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }

    private void HandleFacing()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (facingRight)
            {
                Flip();
                facingRight = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (!facingRight)
                {
                    Flip();
                    facingRight = true;
                }
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.


        // Multiply the player's x local scale by -1.
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1;
        gameObject.transform.localScale = newScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crow")
        {
            if (IsAboveEnemy())
            {
                Destroy(collision.gameObject);
                GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().AddScore();
            }
            else if(!isInvulnerable)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);  
            }              
        }
        else if(collision.gameObject.tag == "Worm")
        {
            Destroy(collision.gameObject);
            GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().AddHP();
            FindObjectOfType<AudioManager>().Play("Bonus");
        }
        else if (collision.gameObject.tag == "Boots")
        {
            Destroy(collision.gameObject);
            speedUp = true;
            speedUpTimer.Run();
            Instantiate(Resources.Load("SpeedUpSliderTimer"));
            FindObjectOfType<AudioManager>().Play("Bonus");
        }
        else if (collision.gameObject.tag == "Seeds")
        {
            Destroy(collision.gameObject);
            shooting = true;
            shootTimer.Run();
            Instantiate(Resources.Load("ShootingSliderTimer"));
            FindObjectOfType<AudioManager>().Play("Bonus");
        }
    }

    private void OnBecameInvisible()
    {
        GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().DecreaseHP();
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public float speed = 5.0f;
    public float bulletSpeed = 7.5f;
    public float bulletLifespan = 2.5f;
    public float ammoCounter = 0;
    public float maxAmmoSize = 1000000000000000000000000000f;
    public float refillAmmoPercentage = .1f;
    private float currentRateOfFire;
    public float slowRateOfFire = .3f;
    public float fastRateOfFire = .0000000000000000000000000000005f;
    private float rofTimer = 0;
    private float invincibilityTimer = 0;
    public float invincibilityDuration = 2;

    public bool invincible = false;
    public bool canShoot = true;
    public bool fireMode = true; // True == fastRateOfFire | False == slowRateOfFire

    GameManager gm;
    public GameObject bullet;
    Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        myRB = GetComponent<Rigidbody2D>();

        

        currentRateOfFire = fastRateOfFire;

        ammoCounter = maxAmmoSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = myRB.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * speed;
        velocity.y = Input.GetAxisRaw("Vertical") * speed;
        
        if (health < 1)
        {
            health = 3;
            transform.SetPositionAndRotation(new Vector2(), new Quaternion());
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (fireMode)
            {
                currentRateOfFire = slowRateOfFire;
                fireMode = false;
            }
            else
            {
                currentRateOfFire = fastRateOfFire;
                fireMode = true;
            }
        }

        

        if (ammoCounter >= 1 && canShoot)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                ammoCounter--;

                

                GameObject b = Instantiate(bullet, transform);
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), b.GetComponent<CircleCollider2D>());
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);

                canShoot = false;

                Destroy(b, bulletLifespan);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                ammoCounter--;

                

                GameObject b = Instantiate(bullet, transform);
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), b.GetComponent<CircleCollider2D>());
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);

                canShoot = false;

                Destroy(b, bulletLifespan);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                ammoCounter--;

               

                GameObject b = Instantiate(bullet, transform);
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), b.GetComponent<CircleCollider2D>());
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);

                canShoot = false;

                Destroy(b, bulletLifespan);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                ammoCounter--;

                

                GameObject b = Instantiate(bullet, transform);
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), b.GetComponent<CircleCollider2D>());
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);

                canShoot = false;

                Destroy(b, bulletLifespan);
            }
        }

        if(canShoot == false)
        {
            rofTimer += Time.deltaTime;

            if (rofTimer >= currentRateOfFire)
            {
                canShoot = true;
                rofTimer = 0;
            }
        }

        if (invincible == true)
        {
            invincibilityTimer += Time.deltaTime;

            if(invincibilityTimer >= invincibilityDuration)
            {
                invincible = false;
                invincibilityTimer = 0;
            }
        }

        myRB.velocity = velocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.tag == "enemy")
            {

                health--;
                invincible = true;
            }

           
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("end"))
        {
            gm.playerOnEnd = true;
        }
    }
}

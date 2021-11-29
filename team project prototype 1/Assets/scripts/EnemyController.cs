using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 1;
    public float speed = 2.0f;

    public bool advMode = false;
    public bool isFollowing = false;

    public bool moveOnY = true;
    public bool startMovePos = true;
    public float maxDistance = 5.0f;

    Vector2 zero;
    Vector2 origin;
    Vector2 maxPos;
    Vector2 minPos;

    public Transform playerTarget;
    Rigidbody2D myRB;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        gm = GetComponent<GameManager>();
        if (advMode == false)
        {
            origin = transform.position;

            if (moveOnY)
            {
                myRB.constraints = RigidbodyConstraints2D.FreezePositionX &
                                   RigidbodyConstraints2D.FreezeRotation;

                maxPos = new Vector2(origin.x, origin.y + maxDistance);
                minPos = new Vector2(origin.x, origin.y - maxDistance);

                if (startMovePos)
                {
                    myRB.velocity = new Vector2(0, speed);
                }
                else
                {
                    myRB.velocity = new Vector2(0, -speed);
                }
            }
            else
            {
                myRB.constraints = RigidbodyConstraints2D.FreezePositionY &
                                   RigidbodyConstraints2D.FreezeRotation;

                maxPos = new Vector2(origin.x + maxDistance, origin.y);
                minPos = new Vector2(origin.x - maxDistance, origin.y);

                if (startMovePos)
                {
                    myRB.velocity = new Vector2(speed, 0);
                }
                else
                {
                    myRB.velocity = new Vector2(-speed, 0);
                }
            }
        }
        else
        {
            zero = new Vector2(0, 0);
            playerTarget = GameObject.Find("Player").transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health < 1)
            Destroy(gameObject);

        if (advMode == false)
        {
            if (moveOnY)
            {
                if (transform.position.y >= maxPos.y)
                    myRB.velocity = new Vector2(0, -speed);
                else if (transform.position.y <= minPos.y)
                    myRB.velocity = new Vector2(0, speed);
            }
            else
            {
                if (transform.position.x >= maxPos.x)
                    myRB.velocity = new Vector2(-speed, 0);
                else if (transform.position.x <= minPos.x)
                    myRB.velocity = new Vector2(speed, 0);
            }
        }
        else
        {
            if (isFollowing == false)
                myRB.velocity = zero;
            else
            {
                Vector3 lookPos = playerTarget.position - transform.position;
                float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                myRB.rotation = angle;
                lookPos.Normalize();

                myRB.MovePosition(transform.position + (lookPos * speed * Time.deltaTime));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerBullet_basic")
        {
            Destroy(collision.gameObject);
            health--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player") && advMode)
            isFollowing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player") && advMode)
            isFollowing = false;
    }
}

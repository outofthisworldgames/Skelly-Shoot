using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public int health = 3;
    public float speed = 5;




    Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        

    }




}

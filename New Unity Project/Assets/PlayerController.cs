using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    public bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("ground"))
        {
            grounded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = rigid.velocity;
        if (Input.GetKeyDown(KeyCode.A))
        {
            velocity.x -= 1;
            if(velocity.x <= -3)
            {
                velocity.x = -3;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            velocity.x += 1;
            if (velocity.x <= 3)
            {
                velocity.x = 3;
            }
        }
        if(grounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y += 1;
                if (velocity.y <= 3)
                {
                    velocity.y = 3;
                }
                grounded = false;
            }
        }
            
        rigid.velocity = velocity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climb : MonoBehaviour
{
    [SerializeField] float LadderSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if(rb)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = Vector2.up * LadderSpeed;
            }

        }
    }
}

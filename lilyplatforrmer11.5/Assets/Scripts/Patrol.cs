using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    float timer = 0;
    [SerializeField] float patrolDuration = 1;
    float speed= 6;
    float direction = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = patrolDuration;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            direction *= -1;
            timer = patrolDuration;
        }

        Vector3 step = Vector3.right * direction * speed * Time.deltaTime;
        gameObject.transform.position += step;
    }
}

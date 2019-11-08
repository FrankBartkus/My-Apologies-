using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]int health = 3;
    float iframetimer = 0;
    float iframeDuration = 1;

    [SerializeField] bool isPlayer_ = false;

    Vector3 Checkpoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        print("health: " + health);
        SetCheckPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (iframetimer > 0)
        {
            iframetimer -= Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet" && iframetimer <= 0)
        {
            health -= 1;
            print("Health: " + health);
            iframetimer = iframeDuration;

            DeathCheck();
        }
    }

    public void SetCheckPoint()
    {
        Checkpoint = gameObject.transform.position;
    }


    void DeathCheck()
    {
        if (health == 0)
        {
            //GameObject.Destroy(gameObject);

            if (isPlayer_ == true)
            {
                health = 5;
                transform.position = Checkpoint;
            }
            // Set health to max
            // move this object back to the checkpoint

            // else
            // destroy this object

        }
    }
}

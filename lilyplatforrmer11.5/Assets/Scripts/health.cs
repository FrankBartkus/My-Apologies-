using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    int Health = 5;
    float iframetimer = 0;
    float iframeDuration = 1;

    [SerializeField] bool isPlayer_ = false;

    Vector3 Checkpoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        print("Health: " + Health);
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

    public bool Damage(int damage)
    {
        Health -= damage;
        print("Health: " + Health);
        iframetimer = iframeDuration;

        return deathCheck();
    }
    
    public void SetCheckPoint()
    {
        Checkpoint = gameObject.transform.position;
    }


    bool deathCheck()
    {
        if (Health <= 0)
        {
            //GameObject.Destroy(gameObject);

            if (isPlayer_ == true)
            {
                Health = 5;
                transform.position = Checkpoint;
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
            // Set health to max
            // move this object back to the checkpoint

            // else
            // destroy this object
        

            return true;
        }
        else return false;
    }
}

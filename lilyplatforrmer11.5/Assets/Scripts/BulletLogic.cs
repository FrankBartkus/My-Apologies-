using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public int DamageAmount { set { damage_ = value; } }
    int damage_ = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health healthComponent = collision.gameObject.GetComponent<health>();

        if(healthComponent)
        {
            healthComponent.Damage(damage_);
        }
    }
}

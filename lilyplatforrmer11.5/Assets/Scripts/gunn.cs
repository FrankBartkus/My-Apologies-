using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunn : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab = null;

    [SerializeField] float BulletSpeed = 10.0f;

    [SerializeField] [Min(0)] int damage_ = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(BulletPrefab);
            Vector2 fireVelocity = GetComponent<PlayerController>().FacingDirection * Vector2.right * BulletSpeed;

            bullet.GetComponent<Rigidbody2D>().velocity = fireVelocity;
            bullet.transform.position = gameObject.transform.position;

           BulletLogic bL = bullet.GetComponent<BulletLogic>();

            if (bL)
            {
                bL.DamageAmount = damage_;
            }
        }
    }
}

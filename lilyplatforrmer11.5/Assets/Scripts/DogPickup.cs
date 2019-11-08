using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPickup : MonoBehaviour
{
    GameObject dogRef_ = null;
    [SerializeField] KeyCode pickUpKey_ = KeyCode.P;

    bool pickupFrame_ = false;

    // Update is called once per frame
    void Update()
    {
        dogDrop();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        dogPickup(collision.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        dogPickup(other.gameObject);
    }

    void dogCarrying()
    {
        dogRef_.transform.position = gameObject.transform.position;
    }

    /// <summary>
    /// if the player is holding a dog, they will drop it
    /// </summary>
    void dogDrop()
    {
        if (dogRef_)
        {
            dogRef_.transform.position = gameObject.transform.position;

            if(!pickupFrame_ && (Input.GetKeyDown(pickUpKey_)))
            {
                Rigidbody2D dogRb = dogRef_.GetComponent<Rigidbody2D>();
                Collider2D dogCollider = dogRef_.GetComponent<Collider2D>();

                if (dogRb) dogRb.bodyType = RigidbodyType2D.Dynamic;
                if (dogCollider) dogCollider.enabled = true;

                pickupFrame_ = true;
                dogRef_ = null;
            }
        }

        if (pickupFrame_) pickupFrame_ = false;
    }

    /// <summary>
    /// used to pick up a dog, if the player is not already holding one
    /// </summary>
    /// <param name="dog"></param>
    void dogPickup(GameObject dog)
    {
        if (!dogRef_)
        {
            if (dog.tag == "Dog")
            {
                if (Input.GetKeyDown(pickUpKey_) && !pickupFrame_)
                {
                    dogRef_ = dog.gameObject;
                    Rigidbody2D dogRb = dogRef_.GetComponent<Rigidbody2D>();
                    Collider2D dogCollider = dogRef_.GetComponent<Collider2D>();

                    if (dogRb) dogRb.bodyType = RigidbodyType2D.Static;
                    if (dogCollider) dogCollider.enabled = false;

                    dogRef_.transform.position = gameObject.transform.position;

                    pickupFrame_ = true;
                }
            }
        }
    }
}

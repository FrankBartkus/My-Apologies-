using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    [SerializeField] float minimumSpeed_ = 1.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var otherRB = collision.transform.GetComponent<Rigidbody2D>();

        if(otherRB)
        {
            otherRB.velocity = (otherRB.velocity.normalized * Mathf.Min(minimumSpeed_, otherRB.velocity.magnitude));
        }
    }
}

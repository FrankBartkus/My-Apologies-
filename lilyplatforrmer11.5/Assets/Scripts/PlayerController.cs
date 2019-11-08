using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles RigidBody2D movement for a player character, including running and jumping.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] KeyCode moveKeyRight_ = KeyCode.D;
    [SerializeField] KeyCode moveKeyLeft_ = KeyCode.A;
    [SerializeField] KeyCode moveKeyJump_ = KeyCode.Space;

    [Tooltip("The max speed the character moves")]
    [Min(0)]
    [SerializeField] float speed_ = 10.0f;

    [Tooltip("How high the charater jumps")]
    [Min(0)]
    [SerializeField] float jumpStrength_ = 10.0f;

    [Tooltip("How much influence the held jump plays in the jump height.")]
    [Range(0, 1)]
    [SerializeField] float jumpHoldInfluence_ = 0.75f;

    [Tooltip("How long the jump button can be held for")]
    [Min(0)]
    [SerializeField] float jumpHoldDuration_ = 0.1f;

    [Tooltip("How much movement control the player has while airborn")]
    [Range(0, 1)]
    [SerializeField] float inAirControlSpeed_ = 0.5f;

    float acceleration_ = 0.1f; // how long it takes the player to reach full speed
    float reverseScalar_ = 3.0f; // how much faster a character turns around
    float decelerationScalar_ = 3.0f; // how much faster a character stops moving

    float jumpHoldTimer_ = 0.0f; // used to track how long the jump button can be held down
    bool jumpReleased_ = true; // used to track the player releasing the jump button
    bool grounded_ = false; // is true while the player in on the ground

    /// <summary>
    /// Gets the current direction the charater is facing
    /// </summary>
    public Vector2 FacingDirection { get { return facingDirection_; } }
    Vector2 facingDirection_ = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = movementInputCheck();
        walkingUpdate(input);

        grounded_ = false;
    }

    /// <summary>
    /// Translates user input to a direction
    /// </summary>
    /// <returns>vertical and horizontal movement input</returns>
    Vector2 movementInputCheck()
    {
        Vector2 returnValue = Vector2.zero;
        
        if(Input.GetKey(moveKeyRight_)) // right input
        {
            returnValue += new Vector2(1.0f, 0);
        }
        if (Input.GetKey(moveKeyLeft_)) // left input
        {
            returnValue += new Vector2(-1.0f, 0);
        }
        if(Input.GetKey(moveKeyJump_)) // jump input
        {
            returnValue += new Vector2(0, 1.0f);
        }

        return returnValue;
    }

    /// <summary>
    /// moves the character according to input and current state
    /// </summary>
    /// <param name="moveInput">Movement input: x = horizontall movement, y = vertical movement</param>
    void walkingUpdate(Vector2 moveInput)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        float currentSpeed = Vector2.Dot(rb.velocity, Vector2.right);

        if(moveInput.x != 0) // If the player is requesting the character to move
        {
            float inputDir = (moveInput.x > 0 ? 1.0f : -1.0f);
            float moveDir = (currentSpeed >= 0 ? 1.0f : -1.0f);
            bool isReversing = (currentSpeed != 0 ? (Mathf.Abs(inputDir + moveDir) > 1 ? true : false ) : false);
            
            float step = moveInput.x * (1.0f/acceleration_) * Time.deltaTime * speed_ * (grounded_ ? 1.0f : inAirControlSpeed_) * (isReversing ? reverseScalar_ : 1.0f);

            float projectedSpeed = currentSpeed + step;

            rb.velocity = new Vector2(Mathf.Clamp(projectedSpeed, -speed_, speed_), rb.velocity.y);
            facingDirection_.x = moveDir;
        }
        else // Decelerate if the player is not being asked to move
        {
            if (grounded_)
            {
                if (currentSpeed != 0)
                {
                    float moveDir = (currentSpeed >= 0 ? 1.0f : -1.0f);
                    float step = -moveDir * (1.0f / acceleration_) * Time.deltaTime * speed_ * decelerationScalar_;

                    float projectedSpeed = currentSpeed + step;

                    if (currentSpeed > 0 ? projectedSpeed <= 0 : projectedSpeed >= 0)
                    {
                        projectedSpeed = 0;
                    }

                    rb.velocity = new Vector2(projectedSpeed, rb.velocity.y);
                }
            }
        }
        
        if(moveInput.y != 0) // if the player is requesting a jump
        {
            if(grounded_) // if the player is grounded
            {
                // jump by the minimum amount
                float calcJumpStrength = jumpStrength_ * (1 - jumpHoldInfluence_);
                rb.velocity = new Vector2(rb.velocity.x, calcJumpStrength);
                jumpHoldTimer_ = jumpHoldDuration_;
                jumpReleased_ = false;
                grounded_ = false;
                facingDirection_.y = 1;
            }
            if(!jumpReleased_ && jumpHoldTimer_ > 0) // if the player just jumped and is still holding the jump button
            {
                // increase jump velocity based on how long the button has been held down
                float calcJumpStrength = jumpStrength_ * jumpHoldInfluence_;
                float jumpHoldScale = (Time.deltaTime < jumpHoldTimer_ ? Time.deltaTime : jumpHoldTimer_);
                jumpHoldScale *= (Time.deltaTime / jumpHoldDuration_);
                jumpHoldTimer_ -= Time.deltaTime;
                
                rb.velocity = new Vector2(rb.velocity.x, jumpStrength_ + (calcJumpStrength * ((jumpHoldDuration_ - jumpHoldTimer_) / jumpHoldDuration_)));
                facingDirection_.y = 1;
            }
        }
        else // aknowledge the jump button has been released
        {
            jumpHoldTimer_ = 0.0f;
            jumpReleased_ = true;

            if (grounded_)
            {
                facingDirection_.y = 0;
            }
            else facingDirection_.y = -1;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        groundedCheck(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        groundedCheck(collision);
    }

    /// <summary>
    /// Checks if this character is touching ground
    /// </summary>
    /// <param name="collision"></param>
    void groundedCheck(Collision2D collision)
    {
        // get the point of contact
        Vector3 ctPt = collision.GetContact(0).point;

        Vector2 bounds = new Vector2(gameObject.transform.lossyScale.x/2, gameObject.transform.lossyScale.y/2) * 0.95f;
        Vector3 contactDelta = ctPt - gameObject.transform.position;
        
        // if the point we touched is below us
        if(ctPt.y <= transform.position.y)
        {
            grounded_ = true;
        }
    }
}
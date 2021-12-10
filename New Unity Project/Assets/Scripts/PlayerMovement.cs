using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider2D collider;
    private float dirX = 0f;
    [SerializeField] private float speedConstant = 5f;
    [SerializeField] private float jumpConstant = 3f;
    [SerializeField] private LayerMask ground;
    private enum MovementState { idle, running, jumping, falling}

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(dirX * speedConstant, rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpConstant);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rigidBody.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rigidBody.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }
   
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(
            collider.bounds.center, 
            collider.bounds.size, 
            0f, Vector2.down, 
            .1f, ground);
    }
}

/*
    Input.GetKeyDown("space");
 */
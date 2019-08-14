using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float maxJumpTime = 1f;
    public Transform groundCheck;
    public Animator animator;

    private float jumpTime = 0f;
    private bool isGrounded = false;
    private float curSpeed = 0f;
    private bool isMoving = false;
    private bool pressedJumpButton = false;
    private bool shoot = false;
    private bool jump = false;
    private Rigidbody2D rigBody;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isMoving = Input.GetKey(KeyCode.RightArrow))
        {
            curSpeed = speed;
            spriteRenderer.flipX = false;
        }
        else if (isMoving = Input.GetKey(KeyCode.LeftArrow))
        {
            curSpeed = -speed;
            spriteRenderer.flipX = true;
        }
        else
            curSpeed = 0f;

        shoot = Input.GetKeyDown(KeyCode.Space);
        pressedJumpButton = Input.GetKey(KeyCode.UpArrow);

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("shoot", shoot);
        animator.SetBool("isJumping", !isGrounded);
    }

    private void FixedUpdate()
    {
        rigBody.velocity = new Vector2(curSpeed, rigBody.velocity.y);

        if (!jump)
        {
            jumpTime = 0f;
            isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            jump = isGrounded && pressedJumpButton;
        }
        if (jump)
        {
            jumpTime += Time.fixedDeltaTime;
            Debug.Log(jumpTime);
            if (jump = jumpTime < maxJumpTime && pressedJumpButton)
            {
                rigBody.velocity = new Vector2(rigBody.velocity.x, jumpForce);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public CharacterController controller;

    public Animator animator;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Vector2 player_pastFrame = new Vector2(0, 0);

    [SerializeField]
    private Rigidbody2D rb;

    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;

    private float verticalMove = 0f;
    public float climbingSpeed = 8f;
    private bool isLadder;
    private bool isClimbing;

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Horizontal", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (player_pastFrame.y <= _player.position.y)
        {
            animator.SetFloat("Vertical", 1f);
        }
        else
        {
            animator.SetFloat("Vertical", -1f);
        }

        verticalMove = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(verticalMove) > 0f)
        {
            isClimbing = true;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        player_pastFrame.y = _player.position.y;

        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, verticalMove * climbingSpeed);
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}

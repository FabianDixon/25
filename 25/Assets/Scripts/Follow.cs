using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    [SerializeField] private float m_JumpForce = 400f;

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    private Vector3 m_Velocity = Vector3.zero;

    private bool isFollowing = false;
    private bool jump = false;
    private bool m_FacingRight = true; 

    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = true;
        }
    }

    void Update()
    {
        if (isFollowing)
        {
            //rotate to look at the player
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0); // lock z axis to zero

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                //animator.SetBool("isJumping", true);
            }

            if (Vector3.Distance(transform.position, target.position) > 5f)
            {
                transform.position = new Vector3(target.position.x - 1f, target.position.y, target.position.z - 3f);
                transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
            }
        }
    }

    void FixedUpdate()
    {
        if (isFollowing)
        {
            if (Vector3.Distance(transform.position, target.position) > 1f) //move if distance from target is greater than 1
            {
				Move(speed * Time.fixedDeltaTime, false, jump);
				transform.Translate(new Vector3(speed * Time.fixedDeltaTime, 0, 0));
            }
        }
    }

	private void Move(float move, bool crouch, bool jump)
	{
		
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
		// And then smoothing it out and applying it to the character
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// If the player should jump...
		if (jump)
		{
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

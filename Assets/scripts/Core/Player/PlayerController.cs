using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float sprintSpeed; // Speed when sprinting
    public float speed; // Current speed
    public float jumpingPower;
    public float wallCheckDistance;

    public bool canSprint = true; // Boolean to control whether the player can sprint or not

    private float defaultSpeed; // Default speed
    private float horizontal;
    private float horizontal2;
    private bool isFacingRight = true;

    private bool isSprinting; // Added variable to track sprinting state

    private int jumpsRemaining; // Number of jumps remaining

    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    AudioSource footstepAudioSource;
    [SerializeField] private AudioClip[] footstepSounds; // Array of footstep sounds
    [SerializeField] DynamicJoystick joystick;

    [Header("Script Reference")]
    private PlayerController playerController;
    private PlayerStatus playerStatus;

    [Header("Audio Clip")]
    public AudioClip jumpSound;
    public AudioClip landingSound;

    private Animator anim;
    private bool isPlayingFootstep;

    void Awake()
    {
        playerStatus = GetComponent<PlayerStatus>();
        playerController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        defaultSpeed = speed;
        jumpsRemaining = 2; // Initialize the number of jumps remaining to 2

        // Component Reference for private variables
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        footstepAudioSource = GetComponent<AudioSource>();
        isPlayingFootstep = false;

        // Script Reference for private variables
        
    }

    private void Update()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
        horizontal = joystick.Horizontal;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canSprint) // Check if "Shift" key is pressed and canSprint is true
        {
            isSprinting = true;
            speed = sprintSpeed; // Set the speed to sprint speed
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || !canSprint) // Check if "Shift" key is released or canSprint is false
        {
            isSprinting = false;
            speed = defaultSpeed; // Set the speed back to default
        }

        // Play footstep sound if the player is moving and grounded
        if (Mathf.Abs(horizontal) > 0 && IsGrounded() && !isPlayingFootstep || Mathf.Abs(horizontal2) > 0 && IsGrounded() && !isPlayingFootstep)
        {
            StartCoroutine(PlayFootstepSound());
        }

        
    }

    private void FixedUpdate()
    {
        // Move the player horizontally
        Move();

        
        
    }

    void Move()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", IsGrounded());

        // Set isMoving to true when the player is moving
        if (Mathf.Abs(horizontal) > 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        // Check if facing a wall, then play idle animation
        if (IsFacingWall())
        {
            anim.SetFloat("xVelocity", 0f);
        }

        Flip();
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpsRemaining = 1; // Reset jumps remaining when grounded
            anim.SetTrigger("jump");
            AudioManager.instance.PlaySound(jumpSound);
            
        }
        else if (jumpsRemaining > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpsRemaining--; // Decrease jumps remaining when performing a double jump
            anim.SetTrigger("jump");
            AudioManager.instance.PlaySound(jumpSound);
            
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsFacingWall()
    {
        float direction = isFacingRight ? 1f : -1f;
        Vector2 rayOrigin = new Vector2(wallCheck.position.x, wallCheck.position.y);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, wallCheckDistance, wallLayer); // Change groundLayer to wallLayer

        // Return true if there's a wall in front
        return hit.collider != null;
    }

    

    private IEnumerator PlayFootstepSound()
    {
        isPlayingFootstep = true;
        while (Mathf.Abs(rb.velocity.x) > 0 && IsGrounded())
        {
            AudioClip footstep = footstepSounds[Random.Range(0, footstepSounds.Length)];
            //footstepAudioSource.PlayOneShot(footstep);
            AudioManager.instance.PlaySound(footstep);
            yield return new WaitForSeconds(footstep.length);
        }
        isPlayingFootstep = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            AudioManager.instance.PlaySound(landingSound);
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            Die();
        }
    }

    public void Die()
    {
        anim.SetBool("die", true);
        playerController.enabled = false;
        playerStatus.currHealth = 0;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Die();
        }
    }
}
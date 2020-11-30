using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformerController : MonoBehaviour
{
    #region variables
    //PUBLIC VARIABLES

    //this variable is the movement speed
    public float speed;
    //controls the jump force
    public float jumpForce;

    //if the character is below this y value the scene is reloaded (could be used for game overs)
    public float yToReload;

    //float for input
    private float moveInput;

    //Groundcheck to make sure we can't jump if we're not on the ground
    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask WhatIsGround;
    //incase you want extra jumps, like double jumps
    private int ExtraJumps;
    public int extraJumpValue;

    //PRIVATE VARIABLES

    //bool for flipping the character
    private bool facingRight = true;

    //audiosource
    AudioSource srce;
    //audio clips
    public AudioClip jumpSound;
    //Rigidbody2d ref
    private Rigidbody2D rb;

    #endregion
    #region Methods

    private void Start()
    {  
        //On start, set extra jumps to max extra jumps
        ExtraJumps = extraJumpValue;
        //Get references for rigidbody and audio source
        rb = GetComponent<Rigidbody2D>();
        srce = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //Check for ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);
        //Move character based on input
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

    }
    private void Update()
    {
        //Check for input
        moveInput = Input.GetAxisRaw("Horizontal");

        //If we are grounded
        if (isGrounded)
        {
            //Reset extra jumps
            ExtraJumps = extraJumpValue;
        }
        //IF we have extra jumps and the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && ExtraJumps > 0)
        {
            //jump
            rb.velocity = Vector2.up * jumpForce;
            //subtract the extra jumps
            ExtraJumps--;
            //play a sound
            srce.PlayOneShot(jumpSound);
        }
        //If we don't have any extra jumps but we are grounded
        if (Input.GetKeyDown(KeyCode.Space) && ExtraJumps < 0 && isGrounded == true)
        {
            //jump
            rb.velocity = Vector2.up * jumpForce;
            //play a sound
            srce.PlayOneShot(jumpSound);
        }

        //Check for y position to reload scene
        if(transform.position.y <= yToReload)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //If we are not facing right but moving right
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        //if we are facing right but moving left 
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

    }

    //Method for flipping the character on the y axis
    void Flip()
    {
        //change the bool
        facingRight = !facingRight;
        //flip the character
        transform.Rotate(0, 180, 0);
    }
    #endregion

}

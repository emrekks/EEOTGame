using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerController : NetworkBehaviour
{
    [Header("Player Settings")]
    public float playerSpeed = 3f;
    public float playerJumpHeight = 1f;
    public int playerHealth = 100;

    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    //Dead
    bool playerDeath = false;

    //Gravity
    private Vector3 velocity;
    private float gravity = -9.81f;

    //Descriptions
    private CharacterController controller;
    private Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {  
        Movement();
        Grounded();
    }

    void Movement()
    {
        
        if (!playerDeath)
        {
            //Movement
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = transform.right * horizontal + transform.forward * vertical;
            controller.Move(direction * playerSpeed * Time.deltaTime);

            //Jump
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(playerJumpHeight * -2 * gravity);
            }
        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Grounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Health()
    {
        if(playerHealth <= 0)
        {
            playerDeath = true;
        }

        if (playerDeath)
        {
            //anim.SetBool(death, true);
        }

    }

}

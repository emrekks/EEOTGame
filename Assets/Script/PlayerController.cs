using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerController : NetworkBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float playerJumpHeight = 1f;
    [SerializeField] private int playerHealth = 100;

    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SyncVar]
    bool isGrounded;

    //Dead
    bool playerDeath = false;

    //Gravity
    private Vector3 velocity;
    private float gravity = -9.81f;

    //Descriptions
    private CharacterController controller;
    private Animator anim;

    //AudioListener
    private AudioListener _AudioListener;

    //Camera
    Camera _cam;
    private float lookSpeed = 2.0f;
    private float lookXLimit = 90.0f;
    float rotationX = 0;

    void Start()
    {
        _AudioListener = GetComponentInChildren<AudioListener>();
        _cam = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            _cam.enabled = false;
            _AudioListener.enabled = false;
            return;
        }
        FpsCamera();
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

    

    void FpsCamera()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        _cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
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

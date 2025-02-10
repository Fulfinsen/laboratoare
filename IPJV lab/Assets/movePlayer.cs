using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.EventSystems;
public class movePlayer : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;

    [Header("Movement")]
    CharacterController characterController;
    Animator anim;
    Rigidbody rb;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;


    [SerializeField] float speed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float gravity = 10f;
    [SerializeField] float speedH;
    [SerializeField] float speedV;
    string _currentDirection;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //float curSpeed = speed * Input.GetAxis("Vertical");
        //gameObject.GetComponent<CharacterController>().SimpleMove(forward * curSpeed);

        bool isRunning = anim.GetBool(isRunningHash);
        bool isWalking = anim.GetBool(isWalkingHash);
        bool isJumping = anim.GetBool(isJumpingHash);
        bool forwardPress = Input.GetKey("w");
        bool runningPress = Input.GetKey("left shift");
        bool jumpingPress = Input.GetKey("space");

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;


        // Handle walking animation
        if (!isWalking && forwardPress)
        {
            anim.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPress)
        {
            anim.SetBool(isWalkingHash, false);
        }

        // Handle running animation
        if (!isRunning && (forwardPress && runningPress))
        {
            anim.SetBool(isRunningHash, true);
        }
        if (isRunning && (!forwardPress || !runningPress))
        {
            anim.SetBool(isRunningHash, false);
        }

        if (jumpingPress && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            anim.SetBool(isJumpingHash, true);
        }

        // Check if jump animation has ended
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0); // Assuming layer 0
        if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 1.0f) // Animation has completed
        {
            anim.SetBool(isJumpingHash, false);
        }



        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    void changeDirection(string direction)
    {

        if (_currentDirection != direction)
        {
            if (direction == "right")
            {
                transform.Rotate(0, 180, 0);
                _currentDirection = "right";
            }
            else if (direction == "left")
            {
                transform.Rotate(0, -180, 0);
                _currentDirection = "left";
            }
        }
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount; // Increase the max health
        currentHealth = maxHealth; // Optionally fill health to the new max
        Debug.Log($"Max Health Increased! New Max Health: {maxHealth}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            maxHealth -= 10f;
            gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 10f, ForceMode.Impulse);
            Debug.Log("Ouch");
        }
    }
}
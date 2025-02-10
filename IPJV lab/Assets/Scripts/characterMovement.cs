using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    [Header("References")]
    CharacterController controller;
    [SerializeField] private Transform camera;

    [Header("Movement setting")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float sprintTransitionSpeed = 5f;
    [SerializeField] float turningSpeed = 2f;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float jumpHeight = 2f;

    [Header("Input")]
    float moveInput;
    float turnInput;


    [Header("Health")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;


    [SerializeField] float pushbackForce = 2f;

    float verticalVelocity;
    float speed;

    private HashSet<GameObject> touchedObjects = new HashSet<GameObject>(); // Tracks objects currently being touched


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //movement();
        inputManagement();
        movement();
    }




    void movement()
    {
        groundMovement();
        Turn();
    }

    void groundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        //move = transform.TransformDirection(move); //for currentLookDirection at Turn method

        move = camera.transform.TransformDirection(move);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitionSpeed * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitionSpeed * Time.deltaTime);
        }

        move.y = verticalForceCalculation();
        move *= speed;



        controller.Move(move * Time.deltaTime);
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0)
        {
            //Vector3 currentLookDirection = camera.forward; //move camera based on facing direction
            Vector3 currentLookDirection = controller.velocity.normalized;
            currentLookDirection.y = 0;

            currentLookDirection.Normalize();

            if (currentLookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);
            }
        }
    }

    float verticalForceCalculation()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        return verticalVelocity;
    }

    void inputManagement()
    {
        //Tracking how the character moves (moveInput for vertical movement W/S, turnInput for horizontal movement A/D)
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount; // Increase the max health
        currentHealth = maxHealth; // Optionally fill health to the new max
        Debug.Log($"Max Health Increased! New Max Health: {maxHealth}");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the hit object is the tower (or any other object you want)
        if (hit.collider.CompareTag("Tower"))
        {
            // Calculate the direction to push the player back (horizontal direction only)
            Vector3 pushDirection = (transform.position - hit.transform.position).normalized;

            // Get the player's current vertical velocity (Y-axis)
            float verticalVelocity = controller.velocity.y;

            // Apply the pushback only on the X and Z axes
            Vector3 pushback = new Vector3(pushDirection.x, 0, pushDirection.z) * pushbackForce;

            // Apply the pushback using the CharacterController, preserving the Y-axis (vertical movement)
            controller.Move(pushback * Time.deltaTime);

            // Optionally, you can add the vertical velocity back if needed (in case you're handling jumps)
            controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);

            // Log for debugging
            Debug.Log("Player pushed back by the tower!");
        }
    }
}
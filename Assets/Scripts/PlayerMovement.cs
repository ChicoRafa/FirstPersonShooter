using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the Players ability to move within the scene
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Character Control")]
    public CharacterController characterController;
    public float speed = 10f;

    [Header("Ground Variables")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask isGround;
    public bool isGrounded = false;
    [Header("Gravity")]
    public float gravity = -9.81f;

    [Header("Jump")]
    public float jumpForce = 2.5f;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //checking if we are touching ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, isGround);

        //character movement
        float XAxisMovement = Input.GetAxis("Horizontal");
        float ZAxisMovement = Input.GetAxis("Vertical");
         
        Vector3 moveDirection = transform.right * XAxisMovement + transform.forward * ZAxisMovement;
        characterController.Move(moveDirection * speed * Time.deltaTime);

        //jump logic
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(moveDirection * speed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
    }
}

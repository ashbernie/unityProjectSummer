using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPressure;
    public CharacterController controller;

    private Vector3 originalPosition; // Store the original position of the player

    //gravity and movement variables
    private Vector3 moveDirection;
    public float gravityScale;

    private bool isFalling = false; // Flag to track if the player is falling

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalPosition = transform.position; // Store the original position
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10f) // Check if player falls below a certain y-position
        {
            if (!isFalling) // Check if the player wasn't already falling
            {
                isFalling = true; // Set the falling flag
                Respawn(); // Call the respawn function
            }
        }
        else
        {
            isFalling = false; // Reset the falling flag
        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpPressure;
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);
    }

    // Respawn the player at the original position
    void Respawn()
    {
        transform.position = originalPosition;
        moveDirection = Vector3.zero; // Reset the movement direction
        controller.Move(Vector3.zero); // Reset the character controller's movement
    }
}

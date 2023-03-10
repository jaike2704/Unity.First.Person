using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;

    public float jumphight = 3f;

    // for Crouching
    private bool crouching;
    private float crouchTimer = 0;
    public bool lerpCrouch;

    public float crouchSpeed;


    // for Sprinting
    private bool Sprinting;
    public float sprintSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false; crouchTimer = 0;
            }
        }
    }

    // reeive the inputs for our Inputmanager.cs and aplly them to our character controller.
    public void ProcessMove(Vector3 move)
    {
        Vector3 moveDirection = Vector2.zero;
        moveDirection.x= move.x;
        moveDirection.y= move.y;
        moveDirection.z= move.z;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
    }


    // Different inputs 
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumphight * -3.0f * gravity);
        }
    }


    public void Crouch()
    {
        crouching = !crouching;
        if (crouching)
            speed = crouchSpeed;
        else
            speed = 5;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        Sprinting = !Sprinting;
        if (Sprinting)
            speed = sprintSpeed;
        else
            speed = 5;
    }
}

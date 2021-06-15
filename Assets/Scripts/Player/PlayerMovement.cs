using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=_QajrabyTJc
public class PlayerMovement : MonoBehaviour
{
    public float fowardSpeed = 50f;
    public float sideSpeedMult = 0.75f;
    private float sideSpeed;
    public float backSpeedMult = 0.5f;
    private float backSpeed;
    public float spritSpeedMult = 1.5f;
    public float jumpHeight = 10f;
    public float gravity = 15f;
    public float groundCheckDistance = 0.4f;
    public LayerMask groundMask;
    
    private bool isGrounded;
    private CharacterController controller;
    private Vector3 velocity;
    private Transform groundCheck;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();

        groundCheck = GameObject.Find("Player/GroundCheck").GetComponent<Transform>();

        sideSpeed = fowardSpeed * sideSpeedMult;
        backSpeed = fowardSpeed * backSpeedMult;
    }

    void Update()
    {
        if (!gameObject.GetComponent<PlayerController>().gameOver){
            checkIfGrounded();
            handleWalk();
            handleJump();
            handleGravity();
        }
    }
    private void checkIfGrounded(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }
    private void handleWalk(){
        float moveX = Input.GetAxis("Horizontal") * sideSpeed;
        float moveZ = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.LeftShift) && moveZ > 0 && isGrounded)
            moveZ *= spritSpeedMult; 

        if(moveZ > 0)
            moveZ *= fowardSpeed;
        else if(moveZ < 0)
            moveZ *= backSpeed;

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        controller.Move(moveDirection * Time.deltaTime);
    }

    private void handleJump(){
        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }
            
    }

    private void handleGravity(){
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

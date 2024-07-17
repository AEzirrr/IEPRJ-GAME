using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementBedroom : MonoBehaviour
{
    [Header("Player Model")]
    [SerializeField] private Transform playerModel;

    [Header("Movement")]
    public float moveSpeed;
    public float rotationSpeed;

    public float groundDrag;

    [Header("Attack")]
    public float attackCooldown = 1f; // Duration of attack cooldown
    private bool readyToAttack = true; // Whether the player can attack

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;
    public Transform slashPosition;

    [Header("Raycast")]
    public Transform raycastOrigin; // Custom raycast origin

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public Animator playerAnimation;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true; // Ensure readyToJump is true at start

    }

    private void Update()
    {
        // Use custom raycast origin if set, otherwise default to player's position
        Vector3 rayOrigin = raycastOrigin.position;
        grounded = Physics.Raycast(rayOrigin, Vector3.down, playerHeight * 0.5f, whatIsGround);

        // Debugging ground detection
        Debug.DrawRay(rayOrigin, Vector3.down * (playerHeight * 0.5f + 0.2f), grounded ? Color.green : Color.red);

        MyInput();
        SpeedControl();

        bool isMoving = moveDirection.magnitude > 0.1f;


        if (isMoving && readyToJump)
        {
            if (playerAnimation != null)
            {
                playerAnimation.SetBool("isRunning", true);
                playerAnimation.SetBool("Jumped", false);
            }
        }
        else
        {
            if (playerAnimation != null)
            {
                playerAnimation.SetBool("isRunning", false);
                playerAnimation.SetBool("Jumped", false);
            }
        }

        /////////////////////| ATTACK |\\\\\\\\\\\\\\\\\\\\
        if (Input.GetMouseButtonDown(0) && readyToAttack && TransformProperties.Form == ETransform.HUMAN_FORM)
        {
            playerAnimation.SetBool("Attack", true);
            StartCoroutine(ResetAttackAnimation());
        }


        /////////////////////| JUMP |\\\\\\\\\\\\\\\\\\\\
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            playerAnimation.SetBool("Jumped", true);
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            playerModel.forward = Vector3.Slerp(playerModel.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }


    private void ResetAttack()
    {
        readyToAttack = true;
    }

    private IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        playerAnimation.SetBool("Attack", false);
    }

}

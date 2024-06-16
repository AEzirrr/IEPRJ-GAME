using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    private Vector2 move;

    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce = 7f;
    public float airMultiplier = 5f;
    public float jumpCooldown;
    public float groundDrag = 5f;
    public float raycastHeight = 1f;

    private Rigidbody rb;

    private Animator playerAnimation;

    bool grounded;
    bool readyToJump = true;

    public LayerMask whatIsGround;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        Debug.Log("Move input: " + move);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerAnimation = GetComponent<Animator>();

        if (playerAnimation == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, raycastHeight * 0.5f + 0.2f, whatIsGround);

        // Debugging ground detection
        Debug.DrawRay(transform.position, Vector3.down * (raycastHeight * 0.5f + 0.2f), grounded ? Color.green : Color.red);

        MovePlayer();
        SpeedControl();

        if (move != Vector2.zero)
        {
            if (playerAnimation != null)
            {
                playerAnimation.SetBool("isRunning", true);
                playerAnimation.SetBool("Jumped", false);
                Debug.Log("Run animation triggered!");
            }
        }
        else
        {
            if (playerAnimation != null)
            {
                playerAnimation.SetBool("isRunning", false);
                playerAnimation.SetBool("Jumped", false);
                Debug.Log("Idle animation triggered!");
            }
        }

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            
            Jump();
 
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

    public void MovePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }

        rb.AddForce(movement * speed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply upward force
        Debug.Log(rb.velocity);
    }


    private void ResetJump()
    {
        readyToJump = true;
    }
}

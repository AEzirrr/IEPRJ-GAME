using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    private float nextAttackTime = 0f; // Next time the player can attack

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
    public Transform blockPosition;

    [Header("Raycast")]
    public Transform raycastOrigin; // Custom raycast origin

    [Header("Attack")]
    public GameObject slashEffect;
    public GameObject blockShield;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    [Header("LevelAdvance")]
    private GameObject interactingObject;
    private bool canTeleport = false;

    Rigidbody rb;

    public Animator playerAnimation;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true; // Ensure readyToJump is true at start

        if (slashEffect == null)
        {
            Debug.LogError("Slash effect game object not assigned in " + gameObject.name);
        }
        else
        {
            slashEffect.SetActive(false); // Ensure the slash effect is initially inactive
        }

        if (blockShield == null)
        {
            Debug.LogError("Block Shield game object not assigned in " + gameObject.name);
        }
        else
        {
            blockShield.SetActive(false);
        }
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
        CheckForInteraction();

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

        if (slashEffect != null && playerModel != null)
        {
            slashEffect.transform.rotation = playerModel.rotation;
        }

        // Check if the block shield should be active
        if (Input.GetMouseButton(1) && TransformProperties.Form == ETransform.HUMAN_FORM)
        {
            ActivateBlockShield();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            DeactivateBlockShield();
        }

        if (blockShield != null && blockShield.activeSelf)
        {
            blockShield.transform.position = blockPosition.position;
            blockShield.transform.rotation = playerModel.rotation;
        }

        if (Input.GetMouseButtonDown(0) && readyToAttack && TransformProperties.Form == ETransform.HUMAN_FORM)
        {
            playerAnimation.SetBool("Attack", true);
            ActivateSlashEffect();
            StartCoroutine(ResetAttackAnimation());
        }

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

    private void ActivateSlashEffect()
    {
        if (slashEffect != null && orientation != null && slashPosition != null)
        {
            slashEffect.transform.position = slashPosition.position;
            slashEffect.transform.rotation = Quaternion.LookRotation(orientation.forward);

            slashEffect.SetActive(true);
            Debug.Log("Slash effect activated!");

            Invoke(nameof(DeactivateSlashEffect), 0.2f);
            readyToAttack = false;
            nextAttackTime = Time.time + attackCooldown;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
        else
        {
            Debug.LogError("Slash effect, orientation transform, or slash position not assigned!");
        }
    }

    private void DeactivateSlashEffect()
    {
        if (slashEffect != null)
        {
            slashEffect.SetActive(false);
            Debug.Log("Slash effect deactivated!");
        }
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

    private void ActivateBlockShield()
    {
        if (blockShield != null && orientation != null && blockPosition != null)
        {
            blockShield.transform.position = blockPosition.position;
            blockShield.transform.rotation = playerModel.rotation;

            blockShield.SetActive(true);
            Debug.Log("Block Shield activated!");
        }
        else
        {
            Debug.LogError("Block Shield, orientation transform, or block position not assigned!");
        }
    }

    private void DeactivateBlockShield()
    {
        if (blockShield != null)
        {
            blockShield.SetActive(false);
            Debug.Log("Block Shield deactivated!");
        }
    }

    public void TeleportPlayer(string spawnPointName)
    {
        GameObject spawnPoint = GameObject.Find(spawnPointName);
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
            Debug.Log($"Player teleported to {spawnPointName} at {spawnPoint.transform.position}");
        }
        else
        {
            Debug.LogError($"Spawn point {spawnPointName} not found!");
        }
    }

    private void CheckForInteraction()
    {
        if (canTeleport && Input.GetKeyDown(KeyCode.E))
        {
            HandleTeleportation();
            Debug.Log("Pressed E to teleport.");
        }
    }

    private void HandleTeleportation()
    {
        if (interactingObject != null)
        {
            if (interactingObject.CompareTag("Level1_Exit"))
            {
                TeleportPlayer("Level2_Spawn");
                Debug.Log("Teleported to Level2_Spawn.");
            }
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Level1_Exit"))
        {
            canTeleport = true;
            interactingObject = other.gameObject;

            Debug.Log("Entered collider.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Level1_Exit"))
        {
            canTeleport = false;
            interactingObject = null;

            Debug.Log("Exited collider.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pushable")){
            playerAnimation.SetBool("isPushing", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            playerAnimation.SetBool("isPushing", false);
        }
    }
}

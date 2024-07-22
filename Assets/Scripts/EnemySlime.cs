using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;

public class EnemySlime : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField]
    private float attackDamage;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float maxTimeToReachWalkPoint = 3f;
    private float walkPointTimer;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Knockback
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;

    [SerializeField] Animator animator;


    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene. Make sure the player GameObject is tagged as 'Player'.");
        }


        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        // Commented out the attack code
        // if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            walkPointTimer += Time.deltaTime;

            if (walkPointTimer > maxTimeToReachWalkPoint)
            {
                walkPointSet = false;
                walkPointTimer = 0f;
            }
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            walkPointTimer = 0f;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    // Commented out the attack code
    /*
    private void AttackPlayer()
    {
        // Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            // End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            currentHealth -= 25;
            UpdateHealthUI();
            if (currentHealth <= 0)
            {
                animator.SetBool("isDead", true);
                Invoke(nameof(DestroyEnemy), 1f);
            }

            Destroy(collision.gameObject);

            Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;
            StartCoroutine(ApplyKnockback(knockbackDirection));
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            HealthAndManaManager.Instance.TakeDamage(attackDamage);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthAndManaManager.Instance.TakeDamage(attackDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Slash"))
        {
            Debug.Log("Slash Hit");
            currentHealth -= 50;
            currentHealth -= 25;
            UpdateHealthUI();
            if (currentHealth <= 0)
            {
                animator.SetBool("isDead", true);
                Invoke(nameof(DestroyEnemy), 1f);
            }

            Vector3 knockbackDirection = (transform.position - other.transform.position).normalized;
            StartCoroutine(ApplyKnockback(knockbackDirection));
        }
    }

    private IEnumerator ApplyKnockback(Vector3 direction)
    {
        agent.isStopped = true;
        float elapsedTime = 0f;

        while (elapsedTime < knockbackDuration)
        {
            transform.position += direction * knockbackForce * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        agent.isStopped = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}
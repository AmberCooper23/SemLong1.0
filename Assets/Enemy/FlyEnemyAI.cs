using System.Collections;
using UnityEngine;

public class FlyEnemyAI : MonoBehaviour
{
    public float speed = 2f; // Speed of the enemy
    public float detectionRange = 5f; // Range within which the enemy detects the players
    public float attackSpeed = 3f; // Speed of the enemy when attacking
    public float randomMovementInterval = 3f; // Interval to change random movement direction
    public float bounceBackForce = 5f; // Force applied to bounce back after collision
    public float damageCooldown = 2f; // Cooldown period before the enemy can deal damage again

    private Rigidbody2D rb;
    private Vector2 randomDirection;
    private bool isPlayerInRange = false;
    private bool canDealDamage = true; // Flag to control damage cooldown

    private Transform player1;
    private Transform player2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player1 = GameObject.FindGameObjectWithTag("Player1").transform;
        player2 = GameObject.FindGameObjectWithTag("Player2").transform;
        StartCoroutine(ChangeRandomDirection());
    }

    void Update()
    {
        CheckPlayerInRange();

        if (isPlayerInRange)
        {
            Debug.Log("Player detected, moving towards player.");
            AttackPlayer();
        }
        else
        {
            Debug.Log("No player detected, moving randomly.");
            RandomMovement();
        }
    }

    void RandomMovement()
    {
        rb.velocity = randomDirection * speed;
    }

    void AttackPlayer()
    {
        Transform targetPlayer = GetClosestPlayer();
        if (targetPlayer != null)
        {
            Vector2 direction = (targetPlayer.position - transform.position).normalized;
            rb.velocity = direction * attackSpeed;
            Debug.Log($"Moving towards {targetPlayer.tag}");
        }
    }

    IEnumerator ChangeRandomDirection()
    {
        while (true)
        {
            randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            yield return new WaitForSeconds(randomMovementInterval);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canDealDamage)
        {
            if (collision.gameObject.CompareTag("Player1"))
            {
                Debug.Log("Collision with Player1 detected."); // Debug log for Player1 collision
                ScoreManager.instance.HandleCollision("Player1", -3); // Subtract score for Player1
                StartCoroutine(DamageCooldown());
                BounceBack(collision);
            }
            else if (collision.gameObject.CompareTag("Player2"))
            {
                Debug.Log("Collision with Player2 detected."); // Debug log for Player2 collision
                ScoreManager.instance.HandleCollision("Player2", -3); // Subtract score for Player2
                StartCoroutine(DamageCooldown());
                BounceBack(collision);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Collision with player ongoing."); // Debug log for ongoing collision
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Player exited collision range."); // Debug log for player exiting collision
            isPlayerInRange = false;
        }
    }

    void CheckPlayerInRange()
    {
        float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);

        isPlayerInRange = (distanceToPlayer1 <= detectionRange || distanceToPlayer2 <= detectionRange);

        if (isPlayerInRange)
        {
            Debug.Log("Player detected within range.");
        }
        else
        {
            Debug.Log("No player within detection range.");
        }
    }

    Transform GetClosestPlayer()
    {
        float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);

        if (distanceToPlayer1 < distanceToPlayer2)
        {
            return distanceToPlayer1 <= detectionRange ? player1 : null;
        }
        else
        {
            return distanceToPlayer2 <= detectionRange ? player2 : null;
        }
    }

    void BounceBack(Collision2D collision)
    {
        Vector2 bounceDirection = (collision.transform.position - transform.position).normalized;
        rb.AddForce(-bounceDirection * bounceBackForce, ForceMode2D.Impulse);
    }

    IEnumerator DamageCooldown()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDealDamage = true;
    }
}

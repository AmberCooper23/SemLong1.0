using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1; // Health of the enemy
    public AudioClip hitSound; // Sound to play when enemy is hit

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
    void Die()
    {
        // Add any death effects here (e.g., animations, particles, etc.)
        Destroy(gameObject); // Destroy the enemy game object
    }
}

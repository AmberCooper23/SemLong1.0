using System.Collections;
using UnityEngine;

public class SpatulaCol : MonoBehaviour
{
    private GameObject spatula;
    public float lifeTime = 2f; // Time in seconds before the spatula is destroyed
    public AudioClip hitSound; // Sound to play when spatula hits an enemy

    // Start is called before the first frame update
    void Start()
    {
        spatula = this.gameObject;
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1); // Assume 1 damage per hit
                if (hitSound != null)
                {
                    AudioSource.PlayClipAtPoint(hitSound, transform.position);
                }
            }
        }
        Destroy(spatula); // Destroy the spatula after the collision
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(spatula);
    }
}

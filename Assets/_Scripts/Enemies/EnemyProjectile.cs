using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to player
            var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.DealDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}

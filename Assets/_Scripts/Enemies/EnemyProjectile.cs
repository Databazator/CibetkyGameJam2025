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

    public void HitPlayer(GameObject player)
    {
        // Apply damage to player
        var playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.DealDamage(damage);
        }
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HitPlayer(collision.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

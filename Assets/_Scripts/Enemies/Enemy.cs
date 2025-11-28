using UnityEngine;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 5f;
    public float health = 100f;
    public float damage = 10f;
    public float visibleRange = 40f;
    public float shootingRange = 10f;
    public float attackCooldown = 2f;
    private float lastAttackTime = -2f;
    public float walkCooldown = 3f;
    private float lastWalkingTime = -3f;
    
    public string enemyName;
    public GameObject projectile;
    public GameObject roomArea;
    
    private Vector3 wanderPosition;
    private bool moved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Find the player
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Interact only if in the visible range
        if (distance > visibleRange)
        {
            // If in cooldown, do nothing
            if (Time.time - lastAttackTime < attackCooldown) return;

            Wander();
            return;
        }

        // Player is in distance
        wanderPosition = Vector3.zero; // Reset wander position when engaging player
        
        // If in cooldown, do nothing
        if (Time.time - lastAttackTime < attackCooldown) return;

        // Get to shooting range if not already there
        if (distance > shootingRange - 2f && moved || distance > shootingRange)
        {
            transform.position += direction * speed * Time.deltaTime;
            moved = true;
        }
        else
        {
            // Stop moving when in shooting range
            if (moved)
            {
                moved = false;
                lastWalkingTime = Time.time;
            }
            // When not moving for 0.2 seconds, shoot
            if (Time.time - lastWalkingTime >= 0.2f)
            {
                ShootAtPlayer(player);
            }
        }
    }

    void Wander()
    {
        Debug.Log(enemyName + " is wandering.");
        // Wander to a random direction slowly
        if (wanderPosition == Vector3.zero || Vector3.Distance(transform.position, wanderPosition) < 1f)
        {
            // Don't wander outside the room area
            Vector3 randomDirection = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            Vector3 potentialPosition = transform.position + randomDirection;
            if (roomArea.GetComponent<Collider>().bounds.Contains(potentialPosition))
            {
                wanderPosition = potentialPosition;
            }
            lastWalkingTime = Time.time;
        } else {
            // If not on walking cooldown, move towards wander position
            if (Time.time - lastWalkingTime >= walkCooldown)
            {
                transform.position = Vector3.MoveTowards(transform.position, wanderPosition, speed * Time.deltaTime);
            }
        }
    }

    void ShootAtPlayer(GameObject player)
    {
        Debug.Log(enemyName + " is shooting at the player!");
        // Eject a bullet towards the player
        var bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector3 shootDirection = (player.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().linearVelocity = shootDirection * 20f;
        lastAttackTime = Time.time;
    }
}

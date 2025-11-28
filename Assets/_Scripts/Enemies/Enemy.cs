using UnityEngine;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f;
    public float projectileSpeed = 20f;
    public float visibleRange = 40f;
    public float shootingRange = 10f;
    public float attackCooldown = 2f;
    private float lastAttackTime = -2f;
    public float walkCooldown = 3f;
    private float lastWalkingTime = -3f;
    public int magazineSize = 1;
    public int barrelsCount = 1;
    private int currentAmmo;
    public float magazineDelay = 1f;
    public float coneSize = 0f;

    public string enemyName;
    public GameObject projectile;
    
    public Vector3 roomBottomLeft;
    public Vector3 roomTopRight;

    private Vector3 wanderPosition;
    private bool moved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = magazineSize;
        lastAttackTime = -attackCooldown;
        lastWalkingTime = -walkCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        // Find the player
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) {
            Wander();
            return;
        }

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
        if (currentAmmo == 0 && Time.time - lastAttackTime < attackCooldown) return;

        if (currentAmmo == 0)
        {
            // Cooldown finished, reload
            currentAmmo = magazineSize;
        }

        // Get to shooting range if not already there
        if (distance > shootingRange - 2f && moved || distance > shootingRange)
        {
            Move(direction);
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

    void Move(Vector3 direction)
    {
        GetComponent<CharacterController>().Move(direction * speed * Time.deltaTime);
    }

    void Wander()
    {
        // Wander to a random direction slowly
        if (wanderPosition == Vector3.zero || Vector3.Distance(transform.position, wanderPosition) < 1f)
        {
            Debug.Log(enemyName + " is looking for wander target.");
            // Don't wander outside the room area
            SetNewWanderPosition();
            lastWalkingTime = Time.time;
        }
        else
        {
            Debug.Log(enemyName + " is wandering.");
            // If not on walking cooldown, move towards wander position
            if (Time.time - lastWalkingTime < walkCooldown) return;
            
            Move((wanderPosition - transform.position).normalized);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // stop wandering
        wanderPosition = Vector3.zero;
    }

    void SetNewWanderPosition()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)).normalized * Random.Range(5f, 15f);
        Vector3 potentialPosition = transform.position + randomDirection;

        // Set temporarily y to 0 for bounds checking
        if (potentialPosition.x >= roomBottomLeft.x && potentialPosition.x <= roomTopRight.x &&
            potentialPosition.z >= roomBottomLeft.z && potentialPosition.z <= roomTopRight.z)
        {
            wanderPosition = potentialPosition;
        }
        else
        {
            Debug.Log(enemyName + " tried to set wander position outside room, retrying.");
        }
    }

    void ShootAtPlayer(GameObject player)
    {
        Debug.Log(enemyName + " is shooting at the player!");
        // Eject a bullet towards the player
        if (currentAmmo <= 0 || Time.time - lastAttackTime < magazineDelay) return;

        for (int i = 0; i < barrelsCount; i++)
        {
            ShootSingleBullet(player, i);
        }
        currentAmmo -= barrelsCount;
        lastAttackTime = Time.time;
    }

    void ShootSingleBullet(GameObject player, int barrelIndex)
    {
        Vector3 shootDirection = (player.transform.position - transform.position).normalized;

        // Apply cone spread, divide equally between barrels
        float angleOffset = barrelsCount == 1
            ? 0
            : -coneSize / 2f + (coneSize / (barrelsCount - 1)) * (barrelIndex);
        Quaternion rotation = Quaternion.AngleAxis(angleOffset, Vector3.up);
        shootDirection = rotation * shootDirection;

        // Instantiate projectile
        GameObject bullet = Instantiate(projectile, transform.position + shootDirection * 1.5f, Quaternion.LookRotation(shootDirection));
        // Set speed
        bullet.GetComponent<Rigidbody>().linearVelocity = shootDirection * projectileSpeed;
        // Set bullet damage
        bullet.GetComponent<EnemyProjectile>().damage = damage;
    }
}

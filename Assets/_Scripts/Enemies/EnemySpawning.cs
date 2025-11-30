using UnityEngine;
using Unity.AI.Navigation;
using DG.Tweening;
using System.Collections.Generic;

public class EnemySpawning : MonoBehaviour
{
    public int numberOfEnemies = 5;
    private int remainingEnemies;
    private int toSpawn = 0;
    public GameObject enemyPrefab;
    public float SpawnDistance = 10f;
    private bool defeated = false;
    internal RoomLocking roomLocking;
    private bool spawning = false;

    // In seconds
    public float SpawningDelay = 1f;
    private float lastSpawnTime = 0f;

    public LayerMask SpawnValidGroundLayerMask;
    public GameObject SpawnVFX;
    public float SpawnDuration = 3f;
    public float EnemyEntitySpawnDelay = 2f;
    public float EnemyEntitySpawnHeight = 10f;

    public List<AudioClip> SpawnSoundEffect;

    void Start()
    {
    }

    void Update()
    {
        if(!spawning) return;

        if (Time.time - lastSpawnTime >= SpawningDelay)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        if (!spawning) return;
        if (toSpawn <= 0)
        {
            spawning = false;
            return;
        }

        var randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        var oneLongVectorWithAngle = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle)).normalized;
        var randomPosition = oneLongVectorWithAngle * SpawnDistance;

        //check if position is valid
        RaycastHit hit;
        Vector3 checkOrigin = new Vector3(transform.position.x + randomPosition.x, 50f, transform.position.z + randomPosition.z);
        if (Physics.Raycast(checkOrigin, Vector3.down, out hit, 100f, SpawnValidGroundLayerMask))
        {
            Debug.LogWarning($"Raycast hit: {hit}, {hit.collider.gameObject.name}");
            SpawnEnemySequence(hit.point);

            remainingEnemies++;
            toSpawn--;
        }
        else
        {
            Debug.Log("No hit on raycast");
        }
        
    }

    private void SpawnEnemySequence(Vector3 spawnPos)
    {
        GameObject beamVFX = Instantiate(SpawnVFX, spawnPos, Quaternion.identity);
        //play audio clip
        var effect = SpawnSoundEffect[UnityEngine.Random.Range(0, SpawnSoundEffect.Count)];
        // Change pitch to random offset
        this.PlayClipAt(effect,spawnPos);

        DOVirtual.DelayedCall(EnemyEntitySpawnDelay, () => SpawnEnemyEntity(spawnPos));
    }
    private void SpawnEnemyEntity(Vector3 spawnPos)
    {
        
        //inst enemy
        var enemy = Instantiate(enemyPrefab, spawnPos + Vector3.up * EnemyEntitySpawnHeight, Quaternion.identity); ;
        // Add enemy under the spawner in the room
        enemy.transform.parent = this.transform;
        // Set room
        var behavior = enemy.GetComponent<EnemyBehavior>();
        // Set name
        behavior.enemyName = "Enemy_" + remainingEnemies;
        var health = enemy.GetComponent<EnemyHealth>();
        health.spawner = this;
    }

    private void PlayClipAt(AudioClip clip, Vector3 pos)
    {
        var tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        var aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        // set other aSource properties here, if desired
        aSource.pitch = UnityEngine.Random.Range(-1f, 1f);
        aSource.Play(); // start the sound
        StartCoroutine(AudioFade.FadeOut(new Sound()
        {
            name = "",
            clip = clip,
            volume = 1f,
            pitch = aSource.pitch,
            loop = false,
            source = aSource
        }, 0.4f, Mathf.SmoothStep));
        Destroy(tempGO, clip.length); // destroy object after clip duration
    }

    public void StartWave(RoomLocking locking)
    {
        roomLocking = locking;
        SpawnEnemies();
    }

    public bool IsDefeated()
    {
        return defeated;
    }

    public void DefeatEnemy()
    {
        if (defeated) return;

        remainingEnemies--;
        Debug.Log("Enemy defeated in spawner, remaining: " + remainingEnemies + ".");
        if (remainingEnemies <= 0 && !spawning && !defeated)
        {
            defeated = true;
            Debug.Log("All enemies defeated in spawner.");
            if (roomLocking != null)
            {
                roomLocking.SpawnerDefeated();
            }
        }
    }

    private void SpawnEnemies()
    {
        defeated = false;
        remainingEnemies = 0;
        spawning = true;
        toSpawn = numberOfEnemies;
        Debug.Log("Starting enemy wave with " + numberOfEnemies + " enemies.");
    }
}

using System.Collections;
using UnityEngine;
using System.Collections;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;
    public KeyCode spawnKey = KeyCode.Space;

    [Range(0f, 1f)]
    public float patronChance = 0.3f;

<<<<<<< Updated upstream
    private bool isWaiting = false;

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnNPC();
        }
    }

=======
    [Header("Auto Spawn")]
    public MiniGame_ShakingScript miniGameScript;
    public float minSpawnDelay = 5f;
    public float maxSpawnDelay = 15f;

    private bool waitingToSpawn = false;
    private AIMovement lastServedNPC = null;

    void Update()
    {

        if (!waitingToSpawn && miniGameScript.servedNPC == null && lastServedNPC != null)
        {
            lastServedNPC = null;
            StartCoroutine(SpawnAfterDelay());
        }

        if (miniGameScript.servedNPC != null)
        {
            lastServedNPC = miniGameScript.servedNPC;
        }
    }

    IEnumerator SpawnAfterDelay()
    {
        waitingToSpawn = true;
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(delay);
        SpawnNPC();
        waitingToSpawn = false;
    }

>>>>>>> Stashed changes
    void SpawnNPC()
    {
        GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
        AIMovement ai = npc.GetComponent<AIMovement>();
<<<<<<< Updated upstream

        if (ai != null)
        {
            ai.isPatron = Random.value < patronChance;
        }

        if (!isWaiting)
        {
            StartCoroutine(SpawnCooldown());
        }
    }

    IEnumerator SpawnCooldown()
    {
        isWaiting = true;
        float timer = Random.Range(1f, 2f);
        yield return new WaitForSeconds(timer);
        isWaiting = false;
=======
        if (ai != null)
            ai.isPatron = Random.value < patronChance;
>>>>>>> Stashed changes
    }
}
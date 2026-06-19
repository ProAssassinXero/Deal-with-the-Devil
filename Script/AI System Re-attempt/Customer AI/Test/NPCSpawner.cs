using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public AIMovement ai;
    public Transform spawnPoint;
    public KeyCode spawnKey = KeyCode.Space;

    [Range(0f, 1f)]
    public float patronChance = 0.3f;

    /*void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnNPC();
        }
    }*/

    [Header("Auto Spawn")]
    public MiniGame_ShakingScript miniGameScript;
    public float minSpawnDelay = 5f;
    public float maxSpawnDelay = 15f;

    private bool waitingToSpawn = false;
    private AIMovement lastServedNPC = null;

    private void Start()
    {
        StartCoroutine(SpawnAfterDelay());
    }
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

    void SpawnNPC()
    {
        npcPrefab = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
        ai = npcPrefab.GetComponent<AIMovement>();

        if (ai != null)
        {
            ai.isPatron = Random.value < patronChance;
        }
    }
}
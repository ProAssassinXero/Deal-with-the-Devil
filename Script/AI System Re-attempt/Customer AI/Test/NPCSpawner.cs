using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;
    public KeyCode spawnKey = KeyCode.Space;

    [Range(0f, 1f)]
    public float patronChance = 0.3f;

    private bool isWaiting = false;

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnNPC();
        }
    }

    void SpawnNPC()
    {
        GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
        AIMovement ai = npc.GetComponent<AIMovement>();

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
    }
}
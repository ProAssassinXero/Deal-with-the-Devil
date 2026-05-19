using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;
    public KeyCode spawnKey = KeyCode.Space;

    [Range(0f, 1f)]
    public float patronChance = 0.3f; // 30% chance by default, adjust in Inspector

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
            AIMovement ai = npc.GetComponent<AIMovement>();

            if (ai != null)
                ai.isPatron = Random.value < patronChance;
        }
    }
}
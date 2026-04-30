/*using System.Collections;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{ 
    public Enemy npc;
    public GameObject Enemy;
    public Enemy enemyScript;
    public int targetIndex;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        target = npc.target[targetIndex];
        gameObject.transform.position = Enemy.transform.position;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TheCounter"))
        {
            StartCoroutine(WaitOnTarget());
            targetIndex += 1;
            enemyScript.targetIndex++;
        }
    }

    public IEnumerator WaitOnTarget()
    {
        yield return new WaitForSecondsRealtime(4f);
        
    }
}
*/
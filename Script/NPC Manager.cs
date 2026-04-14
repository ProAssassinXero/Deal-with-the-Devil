using System.Collections;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{ 
    public Enemy npc;
    public Transform[] target;
    public int targetIndex;
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
        npc.Agent.SetDestination(target[targetIndex].position);
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        Debug.Log("adwawefwefWs");
        if (collision.gameObject.CompareTag("Stop") == true)
        {
           
            StartCoroutine(WaitOnTarget());
            targetIndex += 1;
        }
    }

    public IEnumerator WaitOnTarget()
    {
        yield return new WaitForSecondsRealtime(4f);
        
    }
}

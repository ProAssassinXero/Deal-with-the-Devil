using System.Collections;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{ 
    public Enemy npc;
    public GameObject Enemy;
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
        gameObject.transform.position = Enemy.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("adwawefwefWs");
        if (collision.gameObject.CompareTag("Stop"))
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

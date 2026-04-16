using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int targetIndex;
    public int saveTargetIndex;
    public bool destinationUpdate = false;
    public Transform[] target;
    public Transform self;
    NavMeshAgent Agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateUpAxis = false;
        Agent.updateRotation = false;
        targetIndex = 0;
        saveTargetIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(destinationUpdate)
        {
            positon();
        }
        if (targetIndex == target.Length - 1)
        {
            targetIndex = 0;
            saveTargetIndex = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("adwawefwefWs");
       
        if (collision.gameObject.CompareTag("Stop"))
        {
            Agent.SetDestination(self.position);
            StartCoroutine(WaitOnTarget());
            saveTargetIndex++;

        }
    }

    public IEnumerator WaitOnTarget()
    {
        yield return new WaitForSecondsRealtime(0.1f);        
        targetIndex = saveTargetIndex;
        destinationUpdate = true;
    }

    void positon()
    {
        Agent.SetDestination(target[targetIndex].position);
        destinationUpdate = false;
    }
}
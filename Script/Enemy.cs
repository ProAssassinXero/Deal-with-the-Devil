using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public NPC_OrderScript npc_OrderScript;
    public int targetIndex;
    public int saveTargetIndex;
    public bool destinationUpdate = false;
    public Transform[] target;
    public Transform self;
    public NavMeshAgent Agent;

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
            Positon();
        }
        if (targetIndex == target.Length - 1)
        {
            targetIndex = 0;
            saveTargetIndex = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("OrderTarget"))
        {
            Agent.SetDestination(self.position);
            npc_OrderScript.GabiSend();
        }
    }

    public IEnumerator WaitOnTarget()
    {
        yield return new WaitForSecondsRealtime(0.1f);        
        targetIndex = saveTargetIndex;
        destinationUpdate = true;
    }

    void Positon()
    {
        Agent.SetDestination(target[targetIndex].position);
        destinationUpdate = false;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class NPC_QueueManager : MonoBehaviour
{
    public static NPC_QueueManager instance;

    [Tooltip("The world-space position the first queuing NPC walks to while waiting.")]
    public Transform queueStartPoint;

    [Tooltip("Distance between each NPC in the queue line.")]
    public float queueSpacing = 1.5f;

    [Tooltip("Speed at which queuing NPCs shuffle forward after the front leaves.")]
    public int queueMoveSpeed = 3;

    public List<AIMovement> _queue = new List<AIMovement>();
    public int yspace = 3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void Enqueue(AIMovement npc)
    {
        if (!_queue.Contains(npc))
        {
            _queue.Add(npc);
            Debug.Log($"[Queue] {npc.name} joined queue at position {_queue.Count}.");
        }
    }

    public bool IsMyTurn(AIMovement npc)
    {
        return _queue.Count > 0 && _queue[0] == npc;
    }

    public void NotifyOrderReceived(AIMovement npc)
    {
        if (_queue.Count > 0 && _queue[0] == npc)
        {
            _queue.RemoveAt(0);
            Debug.Log($"[Queue] {npc.name} left the counter. {_queue.Count} NPC(s) remaining.");
        }
    }
    public Vector2 GetQueuePosition(AIMovement npc)
    {
        int index = _queue.IndexOf(npc);
        if (index < 0 || queueStartPoint == null)
        {
            return npc.transform.position;
        }
            
        return new Vector2(queueStartPoint.position.x - index * queueSpacing, queueStartPoint.position.y - yspace);
    }
    public int QueueLength => _queue.Count;
}

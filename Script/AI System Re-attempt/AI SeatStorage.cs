using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransformListLast
{
    [SerializeField] public List<List<Transform>> seatList;
}

public class AISeatStorage : MonoBehaviour
{
    public AIWaypointBar AIWaypointBar;
    public CircleCollider2D npcCollider;
    public AIMovement aiMovement;

    public Transform currentSeat; // just store the single hit transform
    public List<Transform> currentSeatGroup; // the list it belongs to
    public bool sat;
    public float seatSpeed;

    private List<List<Transform>> allLists;

    void Start()
    {
        allLists = new List<List<Transform>>
        {
            AIWaypointBar.chairTransform,
            AIWaypointBar.c_ChairToGoTo,
            AIWaypointBar.lC_ChairToGoTo,
            AIWaypointBar.r_ChairToGoTo,
            AIWaypointBar.lR_ChairToGoTo,
            AIWaypointBar.l_ChairToGoTo,
            AIWaypointBar.lL_ChairToGoTo,
            AIWaypointBar.stools,
            AIWaypointBar.upperChairs
        };
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chair")
        {
            currentSeat = collision.transform;

            // Search all lists for the collided transform
            currentSeatGroup = allLists.FirstOrDefault(list => list.Contains(currentSeat));

            if (currentSeatGroup != null)
            {
                Debug.Log("Found seat in group!");
            }
        }
    }
}
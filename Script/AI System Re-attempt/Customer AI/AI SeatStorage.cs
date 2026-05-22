using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AISeatStorage : MonoBehaviour
{
    public AIWaypointBar AIWaypointBar;
    public AIMovement AIMovement;

    public Transform currentSeat;
    public List<Transform> currentSeatGroup;

    public bool seated;
    private List<List<Transform>> allLists;

    private void Start()
    {
        AIWaypointBar = Object.FindAnyObjectByType<AIWaypointBar>();
    }
    void Update()
    {
        if (currentSeat != null && !AIMovement.isLeaving)
        {
            transform.position = currentSeat.position;
        }
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

        if (collision.gameObject.CompareTag("UpChair") || collision.gameObject.CompareTag("LeftChair") || collision.gameObject.CompareTag("RightChair") && !(collision.gameObject.CompareTag("LowerLeftSeatTransition") || collision.gameObject.CompareTag("LowerRightSeatTransition") || collision.gameObject.CompareTag("LowerCenterSeatTransition")))
        {
<<<<<<< Updated upstream
            //if (collision.transform != AIMovement.targetSeat)
              //  return;
=======
            if (Vector2.Distance(transform.position, collision.transform.position) > 1f)
                return;
>>>>>>> Stashed changes

            seated = true;
            currentSeat = collision.transform;
            currentSeatGroup = allLists.FirstOrDefault(list => list.Contains(currentSeat));

            if (currentSeatGroup != null)
            {
                currentSeatGroup.Remove(currentSeat);
                Debug.Log("Seat removed from group!");
            }
        }

        if (collision.gameObject.CompareTag("LowerLeftSeatTransition") || collision.gameObject.CompareTag("LowerRightSeatTransition") || collision.gameObject.CompareTag("LowerCenterSeatTransition"))
        {
            currentSeat = null;
            AIMovement.lower = true;
        }
    }
}
using System.Collections;
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

    void Update()
    {
        if (currentSeat != null)
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
        if (collision.gameObject.CompareTag("UpChair") || collision.gameObject.CompareTag("DownChair") || collision.gameObject.CompareTag("LeftChair") || collision.gameObject.CompareTag("RightChair"))
        {
            currentSeat = collision.transform;

            // Search all lists for the collided transform
            currentSeatGroup = allLists.FirstOrDefault(list => list.Contains(currentSeat));

            if (currentSeatGroup != null)
            {
                Debug.Log("Found seat in group!");
                StartCoroutine(WaitToBeSeated());
            }
        }
    }

    IEnumerator WaitToBeSeated()
    {
        yield return new WaitForSeconds(2f);
        seated = true;
        currentSeatGroup.Remove(currentSeat);
        GetComponent<DialogueManager>().enabled = false;
    }
}
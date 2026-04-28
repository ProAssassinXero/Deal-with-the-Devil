using UnityEngine;

public class AISeatStorage : MonoBehaviour
{
    public CircleCollider2D npcCollider;
    public AIMovement aiMovement;
    public AIWaypointBar AIWaypointBar;
    public Transform currentSeat;
    public bool sat;
    public float seatSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //store the chairs location from the ai movement script
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chair")
        {
            currentSeat = collision.transform;
            
            sat = true;
        }
    }
}

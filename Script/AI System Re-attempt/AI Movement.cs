using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public int speed;
    public int index;
    public bool newtarget;
    public int randomIndex;
    public Transform frontCounter;
    public Transform[] firstTransitions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!newtarget)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, frontCounter.position, speed * Time.deltaTime);
        }
        else
        {
                       gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, firstTransitions[randomIndex].position, speed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        randomIndex = Random.Range(0, firstTransitions.Length);
        if (collision.gameObject.CompareTag("Counter"))
        {
            newtarget = true;
        }
    }       
}

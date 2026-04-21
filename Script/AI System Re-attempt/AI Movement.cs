using Unity.VisualScripting;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public int speed;
    public int index;
    public bool orderReceived;
    public bool decidedArea;
    public bool lower;
    public bool firstTrans;
    public int toChair;
    public int firstRandomIndex;
    public Transform frontCounter;
    public Transform[] firstTransitions;

    [Header("Chairs to go to")]
    public Transform leftChairTransition;
    public Transform centerChairTransition;
    public Transform rightChairTransition;

    [Header("Center Chairs")]
    public Transform[] c_ChairToGoTo;
    [Header("Lower Center Chairs")]
    public Transform[] lC_ChairToGoTo;

    [Header("Right Chairs")]
    public Transform r_ChairToGoTo;
    [Header("Right Lower Chairs")]
    public Transform lR_ChairToGoTo;

    [Header("Left Chairs")]
    public Transform l_ChairToGoTo;
    [Header("Left Lower Chairs")]
    public Transform lL_ChairToGoTo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!orderReceived)
        {
            //Calculation
            Vector2 targetPos = new Vector2(frontCounter.position.x, gameObject.transform.position.y);
            Vector2 nextTargetPos = new Vector2(gameObject.transform.position.x, frontCounter.position.y);
            bool done = false;

            //Movement
            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }
            if (gameObject.transform.position.x == frontCounter.position.x)
            {
                done = true;
            }
            if (done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nextTargetPos, speed * Time.deltaTime);
            }
        }



        else if (firstTrans && !decidedArea && lower == false)
        {
            Vector2 targetPos = new Vector2(centerChairTransition.position.x, gameObject.transform.position.y);
            Vector2 nexttargetpos = new Vector2(gameObject.transform.position.x, centerChairTransition.position.y);
            bool done = false;

            //Movement
            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }
            if (gameObject.transform.position.x == centerChairTransition.position.x)
            {
                done = true;
            }
            if (done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nexttargetpos, speed * Time.deltaTime);
            }
        }



        else if (decidedArea && !lower)
        {
            Vector2 targetPos = new Vector2(c_ChairToGoTo[toChair].position.x, gameObject.transform.position.y);
            Vector2 nexttargetpos = new Vector2(gameObject.transform.position.x, c_ChairToGoTo[toChair].position.y);
            bool done = false;

            //Movement
            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }
            if (gameObject.transform.position.x == c_ChairToGoTo[toChair].position.x)
            {
                done = true;
            }
            if (done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nexttargetpos, speed * Time.deltaTime);
            }
        }



        else if (lower)
        {
            Vector2 targetPos = new Vector2(lC_ChairToGoTo[toChair].position.x, gameObject.transform.position.y);
            Vector2 nexttargetpos = new Vector2(gameObject.transform.position.x, lC_ChairToGoTo[toChair].position.y);
            bool done = false;

            //Movement
            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }
            if (gameObject.transform.position.x == lC_ChairToGoTo[toChair].position.x)
            {
                done = true;
            }
            if (done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nexttargetpos, speed * Time.deltaTime);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        firstRandomIndex = Random.Range(0, firstTransitions.Length);
        toChair = Random.Range(0, c_ChairToGoTo.Length - 1);
        if (collision.gameObject.CompareTag("Counter"))
        {
            orderReceived = true;
            firstTrans = true;
        }
        if (collision.gameObject.CompareTag("CenterSeatTransition"))
        {
            decidedArea = true;
        }
        if (collision.gameObject.CompareTag("LowerSeatTransition"))
        {
            lower = true;
            decidedArea = false;
        }
    }       
}

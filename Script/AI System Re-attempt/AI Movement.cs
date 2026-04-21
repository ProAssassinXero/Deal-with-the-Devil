using Unity.VisualScripting;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public int speed;
    public int index;
    public bool orderReceived;

    public bool centerDecidedArea;
    public bool rightDecidedArea;
    public bool leftDecidedArea;

    public bool lower;
    public bool firstTrans;
    public int toChair;
    public int firstRandomIndex;
    public Transform frontCounter;

    [Header("Chairs to go to")]
    public Transform[] chairTransform;
    //public Transform leftChairTransition;
    //public Transform centerChairTransition;
    //public Transform rightChairTransition;

    [Header("Center Chairs")]
    public Transform[] c_ChairToGoTo;
    [Header("Lower Center Chairs")]
    public Transform[] lC_ChairToGoTo;

    [Header("Right Chairs")]
    public Transform[] rightChairs;

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


        // To transition Points
        else if (firstTrans && !centerDecidedArea && lower == false)
        {
            Vector2 targetPos = new Vector2(chairTransform[firstRandomIndex].position.x, gameObject.transform.position.y);
            Vector2 nextTargetpos = new Vector2(gameObject.transform.position.x, chairTransform[firstRandomIndex].position.y);
            bool done = false;

            //Movement
            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }
            if (gameObject.transform.position.x == chairTransform[firstRandomIndex].position.x)
            {
                done = true;
            }
            if (done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nextTargetpos, speed * Time.deltaTime);
            }
        }



        else if (centerDecidedArea && !lower)
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
        firstRandomIndex = Random.Range(0, chairTransform.Length);
        toChair = Random.Range(0, c_ChairToGoTo.Length - 1);
        if (collision.gameObject.CompareTag("Counter"))
        {
            orderReceived = true;
            firstTrans = true;
        }

        if (collision.gameObject.CompareTag("CenterSeatTransition"))
        {
            centerDecidedArea = true;
        }
        if (collision.gameObject.CompareTag("RightSeatTransition"))
        {
            rightDecidedArea = true;
        }
        if (collision.gameObject.CompareTag("LeftSeatTransition"))
        {
            leftDecidedArea = true;
        }

        if (collision.gameObject.CompareTag("LowerSeatTransition"))
        {
            lower = true;
            centerDecidedArea = false;
        }
    }       
}

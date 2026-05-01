using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public int speed;
    public Vector2 moveSpeed;

    public Vector2 lastFacing;
    public Vector2 lastPosition;
    public Vector2 movementDirection;
    public Transform currentSeat;
    public Transform target;

    public bool sitting;
    public bool orderReceived;
    public bool doneOrder;

    public bool centerDecidedArea;
    public bool rightDecidedArea;
    public bool leftDecidedArea;
    public bool rightStoolArea;
    public bool upperRightDecidedArea;

    public int toLowerCenterChair;

    public bool lower;

    public int toCenterChair;
    public int toRightChair;
    public int toLeftChair;
    public int toRightStool;
    public int toUpperRightChair;

    public int firstRandomIndex;

    public AISeatStorage AISeatStorage;
    public AIWaypointBar AIWaypointBar;

    public List<List<Transform>> listOfSeats;
    public Transform frontCounter;

    [Header("Chairs to go to")]
    public List<Transform> chairTransform;

    [Header("Center Chairs")]
    public List<Transform> c_ChairToGoTo;
    [Header("Lower Center Chairs")]
    public List<Transform> lC_ChairToGoTo;


    [Header("Right Chairs")]
    public List<Transform> r_ChairToGoTo;
    [Header("Lower Right Chairs")]
    public List<Transform> lR_ChairToGoTo;

    [Header("Left Chairs")]
    public List<Transform> l_ChairToGoTo;
    [Header("Left Lower Chairs")]
    public List<Transform> lL_ChairToGoTo;

    [Header("Right Stools")]
    public List<Transform> stools;

    [Header("Upper Right Seats")]
    public List<Transform> upperChairs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPosition = transform.position;
        AIWaypointBar = Object.FindAnyObjectByType<AIWaypointBar>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = gameObject.transform.position * speed;
        Vector2 posBeforeMovement = transform.position;

        if (AIWaypointBar != null)
        {
            frontCounter = AIWaypointBar.frontCounter;
            chairTransform = AIWaypointBar.chairTransform;
            c_ChairToGoTo = AIWaypointBar.c_ChairToGoTo;
            lC_ChairToGoTo = AIWaypointBar.lC_ChairToGoTo;
            r_ChairToGoTo = AIWaypointBar.r_ChairToGoTo;
            lR_ChairToGoTo = AIWaypointBar.lR_ChairToGoTo;
            l_ChairToGoTo = AIWaypointBar.l_ChairToGoTo;
            lL_ChairToGoTo = AIWaypointBar.lL_ChairToGoTo;
            upperChairs = AIWaypointBar.upperChairs;
            stools = AIWaypointBar.stools;
        }

        if (movementDirection != Vector2.zero)
        {
            lastFacing = movementDirection;
        }

        if (Input.GetKeyDown(KeyCode.R) )
        {
            gameObject.transform.position = new Vector2(11.6f, 1f);

            orderReceived = false;
            centerDecidedArea = false;
            rightDecidedArea = false;
            leftDecidedArea = false;
            rightStoolArea = false;
            upperRightDecidedArea = false;


            toLowerCenterChair = 0;

            lower = false;
            toCenterChair = 0;
            toRightChair = 0;
            toLeftChair = 0;
            toUpperRightChair = 0;

            firstRandomIndex = 0;
        }
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
        else if (doneOrder)
        {
            orderReceived = true;
            centerDecidedArea = false;
            rightDecidedArea = false;
            leftDecidedArea = false;
            rightStoolArea = false;
            sitting = false;
            lower = false;
        }

        else if (orderReceived && !centerDecidedArea && !rightDecidedArea && !leftDecidedArea && !rightStoolArea && !upperRightDecidedArea && !lower)
        {
            Vector2 targetPos = new Vector2(chairTransform[firstRandomIndex].position.x, gameObject.transform.position.y);
            Vector2 nextTargetPos = new Vector2(gameObject.transform.position.x, chairTransform[firstRandomIndex].position.y);
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
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nextTargetPos, speed * Time.deltaTime);
            }
        }
        UpperRightChairLogic();
        RightStoolLogic();
        CenterChairLogic();
        RightChairLogic();
        LeftChairLogic();

        CalculateDirection(posBeforeMovement);

        if (AISeatStorage.seated)
        {
            gameObject.transform.position = new Vector2(AISeatStorage.currentSeat.position.x, AISeatStorage.currentSeat.position.y);

            toLowerCenterChair = 0;
            lower = false;
            toCenterChair = 0;
            toRightChair = 0;
            toLeftChair = 0;
            toUpperRightChair = 0;
            firstRandomIndex = 0;

            movementDirection = Vector2.zero;
            Debug.Log("Running");
        }
    }

    private void CalculateDirection(Vector2 posBefore)
    {
        Vector2 displacement = (Vector2)transform.position - posBefore;

        if (displacement.magnitude > 0.0001f)
        {
            Vector2 normalized = displacement.normalized;
            movementDirection = new Vector2(Mathf.Round(normalized.x), Mathf.Round(normalized.y));
        }
        else
        {
            movementDirection = Vector2.zero;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        firstRandomIndex = Random.Range(0, chairTransform.Count);


        if (collision.gameObject.CompareTag("Counter"))
        {
            orderReceived = true;
            firstRandomIndex = Random.Range(0, chairTransform.Count);
        }

        if (collision.gameObject.CompareTag("Chair"))
        {
            sitting = true;
        }

        if (collision.gameObject.CompareTag("CenterSeatTransition"))
        {
            centerDecidedArea = true;
            toCenterChair = Random.Range(0, c_ChairToGoTo.Count);
        }
        if (collision.gameObject.CompareTag("RightSeatTransition"))
        {
            rightDecidedArea = true;
            toRightChair = Random.Range(1, r_ChairToGoTo.Count);
        }
        if (collision.gameObject.CompareTag("LeftSeatTransition"))
        {
            leftDecidedArea = true;
            toLeftChair = Random.Range(1, l_ChairToGoTo.Count);
        }


        if (collision.gameObject.CompareTag("LowerCenterSeatTransition"))
        {
            toLowerCenterChair = Random.Range(0, lC_ChairToGoTo.Count);
            centerDecidedArea = true;
            lower = true;
        }
        if (collision.gameObject.CompareTag("LowerLeftSeatTransition"))
        {
            leftDecidedArea = true;
            lower = true;
        }
        if (collision.gameObject.CompareTag("LowerRightSeatTransition"))
        {
            rightDecidedArea = true;
            lower = true;
        }

        if (collision.gameObject.CompareTag("RightStoolTransition"))
        {
            rightStoolArea = true;
            toRightStool = Random.Range(0, stools.Count);
        }
        if (collision.gameObject.CompareTag("UpperRightSeatsTransition"))
        {
            upperRightDecidedArea = true;
            toUpperRightChair = Random.Range(0, upperChairs.Count);
        }
    }


    private void CenterChairLogic()
    {
        if (centerDecidedArea && !lower)
        {
            Vector2 nextTargetPos = new Vector2(c_ChairToGoTo[toCenterChair].position.x, gameObject.transform.position.y);
            Vector2 targetPos = new Vector2(gameObject.transform.position.x, c_ChairToGoTo[toCenterChair].position.y);
            bool done = false;

            //Movement
            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }
            if (gameObject.transform.position.y == c_ChairToGoTo[toCenterChair].position.y)
            {
                done = true;
            }
            if (done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nextTargetPos, speed * Time.deltaTime);
            }
        }



        else if (centerDecidedArea && lower)
        {
            Vector2 nextTargetPos = new Vector2(lC_ChairToGoTo[toLowerCenterChair].position.x, gameObject.transform.position.y);
            Vector2 targetPos = new Vector2(gameObject.transform.position.x, lC_ChairToGoTo[toLowerCenterChair].position.y);


            bool done = false;

            //Movement
            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }
            if (gameObject.transform.position.y == lC_ChairToGoTo[toLowerCenterChair].position.y)
            {
                done = true;
            }
            if (done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nextTargetPos, speed * Time.deltaTime);
            }
        }
    }

    private void RightChairLogic()
    {
        if (rightDecidedArea && !lower)
        {
            Vector2 nextTargetPos = new Vector2(r_ChairToGoTo[toRightChair].position.x, transform.position.y);
            Vector2 targetPos = new Vector2(transform.position.x, r_ChairToGoTo[toRightChair].position.y);
            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (transform.position.y == r_ChairToGoTo[toRightChair].position.y)
            {
                done = true;
            }

            if (done)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextTargetPos, speed * Time.deltaTime);
            }
        }

        else if (rightDecidedArea && lower)
        {
            Vector2 nextTargetPos = new Vector2(lR_ChairToGoTo[0].position.x, transform.position.y);
            Vector2 targetPos = new Vector2(transform.position.x, lR_ChairToGoTo[0].position.y);


            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (transform.position.y == lR_ChairToGoTo[0].position.y)
            {
                done = true;
            }

            if (done)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextTargetPos, speed * Time.deltaTime);

            }
        }
    }

    private void LeftChairLogic()
    {
        if (leftDecidedArea && !lower)
        {
            Vector2 nextTargetPos = new Vector2(l_ChairToGoTo[toLeftChair].position.x, transform.position.y);
            Vector2 targetPos = new Vector2(transform.position.x, l_ChairToGoTo[toLeftChair].position.y);
            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (transform.position.y == l_ChairToGoTo[toLeftChair].position.y)
            {
                done = true;
            }

            if (done)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextTargetPos, speed * Time.deltaTime);
            }
        }

        else if (leftDecidedArea && lower)
        {
            Vector2 nextTargetPos = new Vector2(lL_ChairToGoTo[0].position.x, transform.position.y);
            Vector2 targetPos = new Vector2(transform.position.x, lL_ChairToGoTo[0].position.y);


            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (transform.position.y == lL_ChairToGoTo[0].position.y)
            {
                done = true;
            }

            if (done)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextTargetPos, speed * Time.deltaTime);
            }
        }
    }
    private void RightStoolLogic()
    {
        if (rightStoolArea)
        {
            Vector2 targetPos = new Vector2(transform.position.x, stools[toRightStool].position.y);
            Vector2 nextTargetPos = new Vector2(stools[toRightStool].position.x, transform.position.y);

            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                Debug.Log(done);
            }

            if (transform.position.y == stools[toRightStool].position.y)
            {
                done = true;
            }

            if (done)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextTargetPos, speed * Time.deltaTime);
            }
        }
    }


    private void UpperRightChairLogic()
    {
        if (upperRightDecidedArea)
        {
            Vector2 nextTargetPos = new Vector2(upperChairs[toUpperRightChair].position.x, transform.position.y);
            Vector2 targetPos = new Vector2(transform.position.x, upperChairs[toUpperRightChair].position.y);

            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                Debug.Log(done);
            }

            if (transform.position.y == upperChairs[toUpperRightChair].position.y)
            {
                done = true;
            }

            if (done)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextTargetPos, speed * Time.deltaTime);
            }
        }
    }
}
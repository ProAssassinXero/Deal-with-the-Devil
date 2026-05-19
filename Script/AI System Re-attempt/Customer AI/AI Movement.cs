using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public int speed;

    public Vector2 lastFacing;
    public Vector2 lastPosition;
    public Vector2 movementDirection;
    public Transform currentSeat;
    public Transform targetSeat;
    public Transform target;

    public bool sitUp;
    public bool sitDown;
    public bool sitLeft;
    public bool sitRight;

    public bool orderReceived;
    public bool doneOrder;
    public bool doneDrinking;

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

    [Header("Exit")]
    public Transform exitWaypoint;
    private bool drinkingStarted = false;
    public bool isLeaving = false;


    public List<Vector2> pathToSeat = new List<Vector2>();
    private bool recordingPath = false;
    public List<Vector2> exitPath;
    private int exitPathIndex = 0;

    [Header("Patron Settings")]
    public bool isPatron = false;
    public bool patronSetupDone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPosition = transform.position;
        AIWaypointBar = Object.FindAnyObjectByType<AIWaypointBar>();

        if (!isPatron)
        {
            NPC_QueueManager.instance?.Enqueue(this);
        }
        else
        {
            orderReceived = true;
            doneOrder = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            exitWaypoint = AIWaypointBar.exitWaypoint;
        }

        if (isPatron && !patronSetupDone && chairTransform != null && chairTransform.Count > 0)
        {
            patronSetupDone = true;
            firstRandomIndex = Random.Range(0, chairTransform.Count);
        }

        if (movementDirection != Vector2.zero)
        {
            lastFacing = movementDirection;
        }

        // --- NEW: while leaving, walk to exit then destroy ---
        if (isLeaving)
        {
            if (exitPath != null && exitPathIndex < exitPath.Count)
            {
                Vector2 wp = exitPath[exitPathIndex];

                transform.position = Vector2.MoveTowards(transform.position, wp, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, wp) < 0.1f)
                {
                    exitPathIndex++;
                }

            }
            else
            {
                gameObject.SetActive(false);
            }

            CalculateDirection(posBeforeMovement);
            return;
        }

        if (!orderReceived)
        {
            if (NPC_QueueManager.instance != null && NPC_QueueManager.instance.IsMyTurn(this))
            {
                Vector2 targetPos = new Vector2(frontCounter.position.x, transform.position.y);
                Vector2 nextTargetPos = new Vector2(transform.position.x, frontCounter.position.y);
                bool done = false;

                if (!done)
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

                if (transform.position.x == frontCounter.position.x)
                    done = true;

                if (done)
                    transform.position = Vector2.MoveTowards(transform.position, nextTargetPos, speed * Time.deltaTime);
            }
            else if (NPC_QueueManager.instance != null)
            {
                Vector2 queuePos = NPC_QueueManager.instance.GetQueuePosition(this);
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    queuePos,
                    NPC_QueueManager.instance.queueMoveSpeed * Time.deltaTime
                );
            }
        }

        else if (orderReceived && !doneOrder)
        {

        }

        else if (orderReceived && doneOrder && !centerDecidedArea && !rightDecidedArea && !leftDecidedArea && !rightStoolArea && !upperRightDecidedArea && !lower)
        {
            if (chairTransform == null || chairTransform.Count == 0) return;

            centerDecidedArea = false;
            rightDecidedArea = false;
            leftDecidedArea = false;
            rightStoolArea = false;
            upperRightDecidedArea = false;
            sitUp = false;
            lower = false;

            Vector2 targetPos = new Vector2(chairTransform[firstRandomIndex].position.x, gameObject.transform.position.y);
            Vector2 nextTargetPos = new Vector2(gameObject.transform.position.x, chairTransform[firstRandomIndex].position.y);

            bool done = false;

            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }

            if (Mathf.Abs(gameObject.transform.position.x - chairTransform[firstRandomIndex].position.x) < 0.05f)
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

        // record position each frame while walking to seat
        if (recordingPath && !AISeatStorage.seated)
        {
            // only add if moved enough to be worth recording
            if (pathToSeat.Count == 0 || Vector2.Distance(transform.position, pathToSeat[pathToSeat.Count - 1]) > 0.1f)
            {
                pathToSeat.Add(transform.position);
            }
        }

        // stop recording on seated
        if (AISeatStorage.seated && recordingPath)
        {
            recordingPath = false;
        }

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

            if (!drinkingStarted)
            {
                drinkingStarted = true;
                StartCoroutine(DrinkingTime());
            }
        }

        if (AISeatStorage.seated && doneDrinking)
        {
            sitUp = false;
            sitDown = false;
            sitLeft = false;
            sitRight = false;

            centerDecidedArea = false;
            rightDecidedArea = false;
            leftDecidedArea = false;
            rightStoolArea = false;
            upperRightDecidedArea = false;
            lower = false;

            // reverse the recorded path, swap last point for exit
            pathToSeat.Reverse();
            if (pathToSeat.Count > 0)
                pathToSeat[pathToSeat.Count - 1] = exitWaypoint.position; // swap end for exit

            exitPath = pathToSeat;
            exitPathIndex = 0;

            AISeatStorage.currentSeatGroup.Add(AISeatStorage.currentSeat);
            AISeatStorage.currentSeat = null;
            AISeatStorage.seated = false;
            isLeaving = true;

            return;
        }
    }

    IEnumerator DrinkingTime()
    {
        int Timer = Random.Range(5, 10);
        yield return new WaitForSeconds(Timer);
        doneDrinking = true;
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
        if (isLeaving) return;
        firstRandomIndex = Random.Range(0, chairTransform.Count);

        if (collision.gameObject.CompareTag("Counter"))
        {
            if (!isPatron)
            {
                orderReceived = true;
                firstRandomIndex = Random.Range(0, chairTransform.Count);
            }
        }

        if (collision.gameObject.CompareTag("UpChair"))
        {
            sitUp = true;
        }
        /*if (collision.gameObject.CompareTag("DownChair"))
        {
            sitDown = true;
        }*/
        if (collision.gameObject.CompareTag("LeftChair"))
        {
            sitLeft = true;
        }
        if (collision.gameObject.CompareTag("RightChair"))
        {
            sitRight = true;
        }

        if (collision.gameObject.CompareTag("CenterSeatTransition"))
        {
            if (!isPatron) doneOrder = false;
            centerDecidedArea = true;
            toCenterChair = Random.Range(0, c_ChairToGoTo.Count);
            targetSeat = c_ChairToGoTo[toCenterChair];
            recordingPath = true;
            pathToSeat.Clear();
        }
        if (collision.gameObject.CompareTag("RightSeatTransition"))
        {
            rightDecidedArea = true;
            toRightChair = Random.Range(0, r_ChairToGoTo.Count);
            targetSeat = r_ChairToGoTo[toRightChair];
            recordingPath = true;
            pathToSeat.Clear();
        }
        if (collision.gameObject.CompareTag("LeftSeatTransition"))
        {
            leftDecidedArea = true;
            toLeftChair = Random.Range(0, l_ChairToGoTo.Count);
            targetSeat = l_ChairToGoTo[toLeftChair];
            recordingPath = true;
            pathToSeat.Clear();
        }
        if (collision.gameObject.CompareTag("RightStoolTransition"))
        {
            rightStoolArea = true;
            toRightStool = Random.Range(0, stools.Count);
            targetSeat = stools[toRightStool];
            recordingPath = true;
            pathToSeat.Clear();
        }
        if (collision.gameObject.CompareTag("UpperRightSeatsTransition"))
        {
            upperRightDecidedArea = true;
            toUpperRightChair = Random.Range(0, upperChairs.Count);
            targetSeat = upperChairs[toUpperRightChair];
            recordingPath = true;
            pathToSeat.Clear();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Counter"))
        {
            NPC_QueueManager.instance?.NotifyOrderReceived(this);
        }
    }


    private void CenterChairLogic()
    {
        if (centerDecidedArea && !lower && !AISeatStorage.seated)
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



        else if (centerDecidedArea && lower && !AISeatStorage.seated)
        {
            Vector2 nextTargetPos = new Vector2(lC_ChairToGoTo[toLowerCenterChair].position.x, gameObject.transform.position.y);
            Vector2 targetPos = new Vector2(gameObject.transform.position.x, lC_ChairToGoTo[toLowerCenterChair].position.y);


            bool done = false;

            //Movement
            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }
            if (Mathf.Abs(gameObject.transform.position.y - lC_ChairToGoTo[toLowerCenterChair].position.y) < 0.05f)
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
        if (rightDecidedArea && !lower && !AISeatStorage.seated)
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

        else if (rightDecidedArea && lower && !AISeatStorage.seated)
        {
            Vector2 nextTargetPos = new Vector2(lR_ChairToGoTo[toRightChair].position.x, transform.position.y);
            Vector2 targetPos = new Vector2(transform.position.x, lR_ChairToGoTo[toRightChair].position.y);


            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (Mathf.Abs(transform.position.y - lR_ChairToGoTo[toRightChair].position.y) < 0.05f)
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
        if (leftDecidedArea && !lower && !AISeatStorage.seated)
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

        else if (leftDecidedArea && lower && !AISeatStorage.seated)
        {
            Vector2 nextTargetPos = new Vector2(lL_ChairToGoTo[toLeftChair].position.x, transform.position.y);
            Vector2 targetPos = new Vector2(transform.position.x, lL_ChairToGoTo[toLeftChair].position.y);


            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (Mathf.Abs(transform.position.y - lL_ChairToGoTo[toLeftChair].position.y) < 0.05f)
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
        if (rightStoolArea && !AISeatStorage.seated)
        {
            Vector2 targetPos = new Vector2(transform.position.x, stools[toRightStool].position.y);
            Vector2 nextTargetPos = new Vector2(stools[toRightStool].position.x, transform.position.y);

            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
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
        if (upperRightDecidedArea && !AISeatStorage.seated)
        {
            Vector2 nextTargetPos = new Vector2(upperChairs[toUpperRightChair].position.x, transform.position.y);
            Vector2 targetPos = new Vector2(transform.position.x, upperChairs[toUpperRightChair].position.y);

            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

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
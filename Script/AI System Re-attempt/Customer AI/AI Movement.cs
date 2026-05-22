using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [Header("Movement")]
    public int speed;

    public Vector2 lastFacing;
    public Vector2 lastPosition;
    public Vector2 movementDirection;

    [Header("Seat Data")]
    public Transform currentSeat;
    public Transform targetSeat;

    public bool seated;
    public bool isLeaving;

    [Header("Seat Facing")]
    public bool sitUp;
    public bool sitDown;
    public bool sitLeft;
    public bool sitRight;

    [Header("Order State")]
    public bool orderReceived;
    public bool doneOrder;
    public bool doneDrinking;

    [Header("Area Decisions")]
    public bool centerDecidedArea;
    public bool rightDecidedArea;
    public bool leftDecidedArea;
    public bool rightStoolArea;
    public bool upperRightDecidedArea;

    public bool lower;

    [Header("Indexes")]
    public int firstRandomIndex;

    public int toCenterChair;
    public int toLowerCenterChair;

    public int toRightChair;
    public int toLowerRightChair;

    public int toLeftChair;
    public int toLowerLeftChair;

    public int toRightStool;
    public int toUpperRightChair;

    [Header("References")]
    public AISeatStorage AISeatStorage;
    public AIWaypointBar AIWaypointBar;

    public Transform frontCounter;
    public Transform exitWaypoint;

    [Header("Chair Lists")]
    public List<Transform> chairTransform;

    [Header("Center Chairs")]
    public List<Transform> c_ChairToGoTo;
    public List<Transform> lC_ChairToGoTo;

    [Header("Right Chairs")]
    public List<Transform> r_ChairToGoTo;
    public List<Transform> lR_ChairToGoTo;

    [Header("Left Chairs")]
    public List<Transform> l_ChairToGoTo;
    public List<Transform> lL_ChairToGoTo;

    [Header("Right Stools")]
    public List<Transform> stools;

    [Header("Upper Right")]
    public List<Transform> upperChairs;

    [Header("Patrons")]
    public bool isPatron;
    public bool patronSetupDone;

    [Header("Exit Path")]
    public List<Vector2> pathToSeat = new List<Vector2>();
    public List<Vector2> exitPath;

    private bool recordingPath;
    private bool drinkingStarted;
    private int exitPathIndex;

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

    void Update()
    {
        Vector2 posBeforeMovement = transform.position;

        SetupWaypointReferences();

        SetupPatron();

        if (movementDirection != Vector2.zero)
            lastFacing = movementDirection;

        if (isLeaving)
        {
            HandleLeaving(posBeforeMovement);
            return;
        }

        if (!orderReceived)
        {
            HandleQueueMovement();
        }
        else if (orderReceived && doneOrder)
        {
            MoveToTransitionPoint();
        }

        CenterChairLogic();
        RightChairLogic();
        LeftChairLogic();
        RightStoolLogic();
        UpperRightChairLogic();

        HandlePathRecording();

        HandleSeatedState();

        CalculateDirection(posBeforeMovement);
    }

    void SetupWaypointReferences()
    {
        if (AIWaypointBar == null) return;

        frontCounter = AIWaypointBar.frontCounter;

        chairTransform = AIWaypointBar.chairTransform;

        c_ChairToGoTo = AIWaypointBar.c_ChairToGoTo;
        lC_ChairToGoTo = AIWaypointBar.lC_ChairToGoTo;

        r_ChairToGoTo = AIWaypointBar.r_ChairToGoTo;
        lR_ChairToGoTo = AIWaypointBar.lR_ChairToGoTo;

        l_ChairToGoTo = AIWaypointBar.l_ChairToGoTo;
        lL_ChairToGoTo = AIWaypointBar.lL_ChairToGoTo;

        stools = AIWaypointBar.stools;
        upperChairs = AIWaypointBar.upperChairs;

        exitWaypoint = AIWaypointBar.exitWaypoint;
    }

    void SetupPatron()
    {
        if (isPatron && !patronSetupDone && chairTransform.Count > 0)
        {
            patronSetupDone = true;
            firstRandomIndex = Random.Range(0, chairTransform.Count);
        }
    }

    void HandleQueueMovement()
    {
        if (NPC_QueueManager.instance != null &&
            NPC_QueueManager.instance.IsMyTurn(this))
        {
            Vector2 targetPos =
                new Vector2(frontCounter.position.x, transform.position.y);

            Vector2 nextTargetPos =
                new Vector2(transform.position.x, frontCounter.position.y);

            bool done = false;

            if (!done)
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    targetPos,
                    speed * Time.deltaTime
                );

            if (Mathf.Abs(transform.position.x - frontCounter.position.x) < 0.05f)
                done = true;

            if (done)
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    nextTargetPos,
                    speed * Time.deltaTime
                );
        }
        else if (NPC_QueueManager.instance != null)
        {
            Vector2 queuePos =
                NPC_QueueManager.instance.GetQueuePosition(this);

            transform.position = Vector2.MoveTowards(
                transform.position,
                queuePos,
                NPC_QueueManager.instance.queueMoveSpeed * Time.deltaTime
            );
        }
    }

    void MoveToTransitionPoint()
    {
        if (centerDecidedArea ||
            rightDecidedArea ||
            leftDecidedArea ||
            rightStoolArea ||
            upperRightDecidedArea)
            return;

        if (chairTransform == null || chairTransform.Count == 0)
            return;

        Vector2 target =
            chairTransform[firstRandomIndex].position;

        if (Mathf.Abs(transform.position.x - target.x) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(target.x, transform.position.y),
                speed * Time.deltaTime
            );
        }
        else
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(transform.position.x, target.y),
                speed * Time.deltaTime
            );
        }
    }

    void HandlePathRecording()
    {
        if (recordingPath && !AISeatStorage.seated)
        {
            if (pathToSeat.Count == 0 ||
                Vector2.Distance(
                    transform.position,
                    pathToSeat[pathToSeat.Count - 1]
                ) > 0.1f)
            {
                pathToSeat.Add(transform.position);
            }
        }

        if (AISeatStorage.seated)
            recordingPath = false;
    }

    void HandleSeatedState()
    {
        if (!AISeatStorage.seated)
            return;

        transform.position =
            AISeatStorage.currentSeat.position;

        movementDirection = Vector2.zero;

        if (!drinkingStarted)
        {
            drinkingStarted = true;
            StartCoroutine(DrinkingTime());
        }

        if (doneDrinking)
        {
            LeaveBar();
        }
    }

    void LeaveBar()
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

        pathToSeat.Reverse();

        if (pathToSeat.Count > 0)
            pathToSeat[pathToSeat.Count - 1] =
                exitWaypoint.position;

        exitPath = pathToSeat;
        exitPathIndex = 0;

        AISeatStorage.currentSeatGroup.Add(
            AISeatStorage.currentSeat
        );

        AISeatStorage.currentSeat = null;
        AISeatStorage.seated = false;

        isLeaving = true;
    }

    void HandleLeaving(Vector2 posBeforeMovement)
    {
        if (exitPath != null &&
            exitPathIndex < exitPath.Count)
        {
            Vector2 wp = exitPath[exitPathIndex];

            transform.position = Vector2.MoveTowards(
                transform.position,
                wp,
                speed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, wp) < 0.1f)
                exitPathIndex++;
        }
        else
        {
            gameObject.SetActive(false);
        }

        CalculateDirection(posBeforeMovement);
    }

    IEnumerator DrinkingTime()
    {
        int timer = Random.Range(5, 10);

        yield return new WaitForSeconds(timer);

        doneDrinking = true;
    }

    void CalculateDirection(Vector2 posBefore)
    {
        Vector2 displacement =
            (Vector2)transform.position - posBefore;

        if (displacement.magnitude > 0.0001f)
        {
            Vector2 normalized = displacement.normalized;

            movementDirection = new Vector2(
                Mathf.Round(normalized.x),
                Mathf.Round(normalized.y)
            );
        }
        else
        {
            movementDirection = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLeaving) return;

        if (collision.CompareTag("Counter"))
        {
            if (!isPatron)
            {
                orderReceived = true;
                firstRandomIndex =
                    Random.Range(0, chairTransform.Count);
            }
        }

        if (collision.CompareTag("CenterSeatTransition"))
        {
            centerDecidedArea = true;

            toCenterChair =
                Random.Range(0, c_ChairToGoTo.Count);

            targetSeat =
                c_ChairToGoTo[toCenterChair];

            recordingPath = true;
            pathToSeat.Clear();
        }

        if (collision.CompareTag("RightSeatTransition"))
        {
            rightDecidedArea = true;

            toRightChair =
                Random.Range(0, r_ChairToGoTo.Count);

            targetSeat =
                r_ChairToGoTo[toRightChair];

            recordingPath = true;
            pathToSeat.Clear();
        }

        if (collision.CompareTag("LeftSeatTransition"))
        {
            leftDecidedArea = true;

            toLeftChair =
                Random.Range(0, l_ChairToGoTo.Count);

            targetSeat =
                l_ChairToGoTo[toLeftChair];

            recordingPath = true;
            pathToSeat.Clear();
        }

        if (collision.CompareTag("RightStoolTransition"))
        {
            rightStoolArea = true;

            toRightStool =
                Random.Range(0, stools.Count);

            targetSeat =
                stools[toRightStool];

            recordingPath = true;
            pathToSeat.Clear();
        }

        if (collision.CompareTag("UpperRightSeatsTransition"))
        {
            upperRightDecidedArea = true;

            toUpperRightChair =
                Random.Range(0, upperChairs.Count);

            targetSeat =
                upperChairs[toUpperRightChair];

            recordingPath = true;
            pathToSeat.Clear();
        }
    }

    void CenterChairLogic()
    {
        if (centerDecidedArea &&
            !lower &&
            !AISeatStorage.seated)
        {
            MoveToSeat(c_ChairToGoTo[toCenterChair]);
        }
        else if (centerDecidedArea &&
                 lower &&
                 !AISeatStorage.seated)
        {
            MoveToSeat(lC_ChairToGoTo[toLowerCenterChair]);
        }
    }

    void RightChairLogic()
    {
        if (rightDecidedArea &&
            !lower &&
            !AISeatStorage.seated)
        {
            MoveToSeat(r_ChairToGoTo[toRightChair]);
        }
        else if (rightDecidedArea &&
                 lower &&
                 !AISeatStorage.seated)
        {
            MoveToSeat(lR_ChairToGoTo[toLowerRightChair]);
        }
    }

    void LeftChairLogic()
    {
        if (leftDecidedArea &&
            !lower &&
            !AISeatStorage.seated)
        {
            MoveToSeat(l_ChairToGoTo[toLeftChair]);
        }
        else if (leftDecidedArea &&
                 lower &&
                 !AISeatStorage.seated)
        {
            MoveToSeat(lL_ChairToGoTo[toLowerLeftChair]);
        }
    }

    void RightStoolLogic()
    {
        if (rightStoolArea &&
            !AISeatStorage.seated)
        {
            MoveToSeat(stools[toRightStool]);
        }
    }

    void UpperRightChairLogic()
    {
        if (upperRightDecidedArea &&
            !AISeatStorage.seated)
        {
            MoveToSeat(upperChairs[toUpperRightChair]);
        }
    }

    void MoveToSeat(Transform seat)
    {
        Vector2 target = seat.position;

        if (Mathf.Abs(transform.position.y - target.y) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(transform.position.x, target.y),
                speed * Time.deltaTime
            );
        }
        else
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(target.x, transform.position.y),
                speed * Time.deltaTime
            );
        }
    }
}

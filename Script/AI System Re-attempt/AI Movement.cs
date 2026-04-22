using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public int speed;
    public bool orderReceived;

    public bool centerDecidedArea;
    public bool rightDecidedArea;
    public bool leftDecidedArea;

    public int toLowerCenterChair;

    public bool lower;

    public int toCenterChair;
    public int toRightChair;
    public int toLeftChair;

    public int firstRandomIndex;
    public Transform frontCounter;

    [Header("Chairs to go to")]
    public Transform[] chairTransform;

    [Header("Center Chairs")]
    public Transform[] c_ChairToGoTo;
    [Header("Lower Center Chairs")]
    public Transform[] lC_ChairToGoTo;


    [Header("Right Chairs")]
    public Transform[] r_ChairToGoTo;
    [Header("Lower Right Chairs")]
    public Transform lR_ChairToGoTo;

    [Header("Left Chairs")]
    public Transform[] l_ChairToGoTo;
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
        else if (!centerDecidedArea && !rightDecidedArea && !leftDecidedArea && lower == false)
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

            CenterChairLogic();
            RightChairLogic();
            LeftChairLogic();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        firstRandomIndex = Random.Range(0, chairTransform.Length);

        toCenterChair = Random.Range(0, c_ChairToGoTo.Length);
        toRightChair = Random.Range(0, r_ChairToGoTo.Length);
        toLeftChair = Random.Range(0, l_ChairToGoTo.Length);

        toLowerCenterChair = Random.Range(0, lC_ChairToGoTo.Length);

        if (collision.gameObject.CompareTag("Counter"))
        {
            orderReceived = true;
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


        if (collision.gameObject.CompareTag("LowerCenterSeatTransition"))
        {
            StartCoroutine(LowerCentre());
        }
        if (collision.gameObject.CompareTag("LowerLeftSeatTransition"))
        {
            StartCoroutine(LowerLeft());
        }
        if (collision.gameObject.CompareTag("LowerRightSeatTransition"))
        {
            StartCoroutine(LowerRight());
        }
    }

    public IEnumerator LowerCentre()
    {
        yield return new WaitForSeconds(1f);
        centerDecidedArea = false;
        lower = true;
    }
    public IEnumerator LowerLeft()
    {
        yield return new WaitForSeconds(1f);
        leftDecidedArea = false;
        lower = true;
    }
    public IEnumerator LowerRight()
    {
        yield return new WaitForSeconds(1f);
        rightDecidedArea = false;
        lower = true;
    }

    private void CenterChairLogic()
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


        else if (centerDecidedArea && !lower)
        {
            Vector2 targetPos = new Vector2(c_ChairToGoTo[toCenterChair].position.x, gameObject.transform.position.y);
            Vector2 nexttargetpos = new Vector2(gameObject.transform.position.x, c_ChairToGoTo[toCenterChair].position.y);
            bool done = false;

            //Movement
            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }
            if (gameObject.transform.position.x == c_ChairToGoTo[toCenterChair].position.x)
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
            Vector2 targetPos = new Vector2(gameObject.transform.position.x, lC_ChairToGoTo[toCenterChair].position.y);
            Vector2 nextTargetPos = new Vector2(lC_ChairToGoTo[toCenterChair].position.x, gameObject.transform.position.y);
            
            bool done = false;

            //Movement
            if (!done)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            }
            if (gameObject.transform.position.x == lC_ChairToGoTo[toCenterChair].position.x)
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
            Vector2 targetPos = new Vector2(r_ChairToGoTo[toRightChair].position.x, transform.position.y);
            Vector2 nextTargetPos = new Vector2(transform.position.x, r_ChairToGoTo[toRightChair].position.y);
            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (transform.position.x == r_ChairToGoTo[toRightChair].position.x)
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
            Vector2 targetPos = new Vector2(transform.position.x, lR_ChairToGoTo.position.y);
            Vector2 nextTargetPos = new Vector2(lR_ChairToGoTo.position.x, transform.position.y);
            
            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (transform.position.x == lR_ChairToGoTo.position.x)
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
            Vector2 targetPos = new Vector2(l_ChairToGoTo[toLeftChair].position.x, transform.position.y);
            Vector2 nextTargetPos = new Vector2(transform.position.x, l_ChairToGoTo[toLeftChair].position.y);
            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (transform.position.x == l_ChairToGoTo[toLeftChair].position.x)
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
            Vector2 targetPos = new Vector2(transform.position.x, lL_ChairToGoTo.position.y);
            Vector2 nextTargetPos = new Vector2(lL_ChairToGoTo.position.x, transform.position.y);
            
            bool done = false;

            if (!done)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (transform.position.x == lL_ChairToGoTo.position.x)
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

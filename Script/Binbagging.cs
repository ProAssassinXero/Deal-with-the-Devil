using System;
using UnityEngine;

public class Binbagging : MonoBehaviour
{
    public int MaxBinbag = 5;
    public int BodyCount = 0;
    public int BinbagCount = 0;
    public CircleCollider2D BinbagCollider;

    public PlayerInteraction playerInteraction;
    public int DisposedBody;

    // Update is called once per frame
    void Start()
    {

    }

    void Update()
    {
        bool isTopTouching = playerInteraction.topCollider.IsTouching(BinbagCollider);
        bool isBottomTouching = playerInteraction.bottomCollider.IsTouching(BinbagCollider);
        bool isLeftTouching = playerInteraction.leftCollider.IsTouching(BinbagCollider);
        bool isRightTouching = playerInteraction.rightCollider.IsTouching(BinbagCollider);

        if ((isTopTouching || isBottomTouching || isLeftTouching || isRightTouching) && BinbagCount < MaxBinbag)
        {
            BinbagCount = 5;
            Debug.Log("Binbag Count: " + BinbagCount);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("body") && BinbagCount > 0)
        {
            SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (collision.gameObject.tag == "body" && !playerInteraction.PlayerAnimator.isDragging && !playerInteraction.PlayerAnimator.isCombatPhase)
            {
                collision.gameObject.tag = "BaggedBody";
                BinbagCount--;
                spriteRenderer.color = Color.lawnGreen;
            }
        }

        if (collision.gameObject.CompareTag("BaggedBody") && !playerInteraction.PlayerAnimator.isCombatPhase && BodyCount < 1 && Input.GetKey(KeyCode.E))
        {
            BodyCount++;
            Destroy(collision.gameObject);
            Debug.Log("Body Count: " + BodyCount);
        }
        if (collision.gameObject.CompareTag("Disposal") && !playerInteraction.PlayerAnimator.isCombatPhase && BodyCount == 1 && Input.GetKey(KeyCode.E))
        {
            BodyCount--;
            DisposedBody++;
            Debug.Log("Body Disposed: " + DisposedBody);
        }
    }
}

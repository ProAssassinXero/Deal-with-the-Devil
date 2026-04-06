using UnityEngine;

public class Binbagging : MonoBehaviour
{
    public int MaxBinbag = 5;
    public int BodyCount = 0;
    public int BinbagCount = 0;
    public CircleCollider2D BinbagCollider;

    public PlayerInteraction playerInteraction;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("body") && BinbagCount > 0)
        {
            BinbagCount--;
            BodyCount++;
            Destroy(collision.gameObject);
            Debug.Log("Binbag Count: " + BinbagCount);
        }
    }
}

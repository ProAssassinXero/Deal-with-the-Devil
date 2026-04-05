using UnityEngine;

public class Binbagging : MonoBehaviour
{
    public int MaxBinbag = 5;
    public int BodyCount = 0;
    public int BinbagCount = 0;
    public Collider2D BinbagCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PolygonCollider2D>().IsTouching(BinbagCollider) && BinbagCount < MaxBinbag)
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

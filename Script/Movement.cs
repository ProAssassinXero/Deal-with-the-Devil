using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5;
    public rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input.GetAxisRaw("Horzontal")
    }
}

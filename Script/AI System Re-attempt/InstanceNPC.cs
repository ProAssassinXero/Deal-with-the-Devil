using UnityEngine;

public class InstanceNPC : MonoBehaviour
{
    public GameObject nPC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(nPC);
        }
    }
}

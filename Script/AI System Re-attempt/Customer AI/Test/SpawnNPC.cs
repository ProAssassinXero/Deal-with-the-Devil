using System.Collections;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Prefab1;
    public bool spam = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && spam == true)
        {
            Instantiate(Prefab1, new Vector2(-10, 0.95f), Quaternion.identity);
            spam = false;
            StartCoroutine(SpamStop());
        }
    }

    IEnumerator SpamStop()
    {
        yield return new WaitForSeconds(1);
        spam = true;
    }
}

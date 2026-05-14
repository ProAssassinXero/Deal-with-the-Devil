using System.Collections;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Prefab1;
    public bool spam;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && spam == true)
        {
            GameObject go = Instantiate(Prefab1, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.Translate(0.95f, -0.49f, 0f);
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

using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public bool Main = false;
    public GameObject Brother;
    public Collider2D Collider;

    public float xStoredClamp = 2;
    public float yStoredClamp = 2;
    public Vector2 StoredCenter = new Vector2(14.25f, 10);

    void Start()
    {
        if (!Main)
        {
            Collider.enabled = false;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            Brother.SetActive(true);
            Brother.GetComponent<Collider2D>().enabled = true;
            Collider.enabled = false;
            gameObject.SetActive(false);
        }
    }
}

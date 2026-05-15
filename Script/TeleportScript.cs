using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Player;
    public float Range = 3.2f;

    bool Debounce = false;
    // Update is called once per frame
    public float Distance(Vector2 Pos1, Vector2 Pos2)
    {
        return Mathf.Abs((Pos1 - Pos2).magnitude);
    }

    GameObject CheckChildren()
    {
        for (int i = 0; i < 2; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                return transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    private void FixedUpdate()
    {
        Debug.Log(Distance(Player.transform.position, transform.position));
        if (!Debounce)
        {
            if (Distance(Player.transform.position, transform.position) < Range)
            {
                GameObject Child = CheckChildren();
                if (Child)
                {
                    Player.transform.position = Child.transform.position;
                    Debounce = true;
                }
            }
        }
        if (Debounce)
        {
            if (Distance(Player.transform.position, transform.position) > Range + 3)
            {
                Debounce = false;
            }
        }
    }
}

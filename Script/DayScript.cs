using UnityEngine;

public class DayScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int DayCount = 1;

    public static DayScript Instanes;
    private void Awake()
    {
        Instanes = this;
        DontDestroyOnLoad(gameObject);
    }

}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class Mixing : MonoBehaviour, IPointerDownHandler,IPointerUpHandler // These are the interfaces the OnPointerUp method requires.
{
    private Vector2 StartPos = Vector2.zero;
    public Slider _slider;


    private Vector3 lastMousePosition;
    private Vector3 mouseVelo;
    private float LastAngle;
    private float CurrentAngle;
    public int MixingCounter = 0;
    public int Distance = 100;

    public float AngleRange = 2f;

    public bool OnHoldDown = false;

    private void Awake()
    {
        StartPos = transform.position;
    }

    bool Approximate(float flo1, float flo2, float Range)
    {
        if (flo1 > flo2 - Range && flo1 < flo2 + Range)
        {
            return true;
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (!OnHoldDown)
        {
            return;
        }
        LastAngle = CurrentAngle;
        mouseVelo = (Input.mousePosition - lastMousePosition) / Time.deltaTime;
        lastMousePosition = Input.mousePosition;
        CurrentAngle = Vector3.SignedAngle(Input.mousePosition, gameObject.transform.position, Vector3.up) * Mathf.Rad2Deg;
        Debug.Log(CurrentAngle);
        
        if (!Approximate(CurrentAngle, LastAngle, AngleRange) && Mathf.Abs((Input.mousePosition - gameObject.transform.position).magnitude) < Distance)
        {
            MixingCounter++;
            _slider.value = MixingCounter;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnHoldDown = true;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnHoldDown = false;
    }
}

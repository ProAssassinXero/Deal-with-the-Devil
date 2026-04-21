using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class Shaker : PhaseManager, IBeginDragHandler, IDragHandler, IEndDragHandler // These are the interfaces the OnPointerUp method requires.
{
    private Vector2 StartPos = Vector2.zero;
    public Slider _slider;

    private bool debounce = false;
    private Vector2 Direaction;

    private Vector3 lastMousePosition;
    private Vector3 mouseVelo;
    private Vector3 mouseDir;
    private Vector3 LastmouseDir;
    public int ShakeCounter = 0;


    public bool Draging = false;

    private void Awake()
    {
        StartPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        debounce = true;
        Draging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    bool Approximate(Vector3 Pos1, Vector3 Pos2, float Range)
    {
        if ((Pos1.x > Pos2.x - Range && Pos1.x < Pos2.x + Range) && (Pos1.y > Pos2.y - Range && Pos1.y < Pos2.y + Range))
        {
            return true;
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (!Draging)
        {
            return;
        }
        
        LastmouseDir = mouseDir;
        mouseVelo = (Input.mousePosition - lastMousePosition) / Time.deltaTime;
        mouseDir = mouseVelo.normalized;
        lastMousePosition = Input.mousePosition;
        Debug.Log(mouseDir);
        if (Approximate(mouseDir,-mouseDir, mouseVelo.magnitude * 0.0012f) && !debounce)
        {
            ShakeCounter++;
            _slider.value = ShakeCounter;
            debounce = true;
        }
        else if (debounce && Approximate(mouseDir, LastmouseDir, mouseVelo.magnitude* 0.0012f) && mouseVelo.magnitude >= 3)
        {
            debounce = false;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = StartPos;
        Draging = false;
    }
}

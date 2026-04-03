using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    void Update()
    {
        FaceTheMouse();
    }

    void FaceTheMouse()
    {
        //Reference
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Calculation
        Vector2 direction = new Vector2(mousePosition.x - gameObject.transform.position.x, mousePosition.y - gameObject.transform.position.y);
        transform.up = direction;    
    }
    
}

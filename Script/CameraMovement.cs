using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Camera;

    public float yClamp = 10;
    public float xClamp = 20;
    public float Speed = 25;

    public GameObject Player;

    private Vector2 Direaction;
    public Vector2 Center = Vector2.zero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Direaction = (Player.transform.position - Camera.transform.position).normalized;
        
    }
    public Vector3 CameraMove;
    private void FixedUpdate()
    {
        CameraMove = Direaction * Speed * Time.deltaTime;
        
        if (Camera.transform.position.x + CameraMove.x >= Center.x + xClamp)
        {
            CameraMove = new Vector3(0, CameraMove.y,0);
            //Camera.transform.position = new Vector3(CameraMove.x - (Center.x + xClamp), Camera.transform.position.y, -10);
        }
        else if (Camera.transform.position.x + CameraMove.x <= Center.x - xClamp)
        {
            CameraMove = new Vector3(0, CameraMove.y, 0);
            //Camera.transform.position = new Vector3(CameraMove.x + (Center.x - xClamp), Camera.transform.position.y, -10);
        }
        if (Camera.transform.position.y + CameraMove.y >= Center.y + yClamp)
        {
            CameraMove = new Vector3(CameraMove.x, 0, 0);
            
        }
        else if (Camera.transform.position.y + CameraMove.y <= Center.y - yClamp)
        {
            CameraMove = new Vector3(CameraMove.x, 0, 0);
            
        }
        if ((Player.transform.position - Camera.transform.position).magnitude < 3)
        {
            CameraMove = new Vector3(0, 0,0);
        }
        Camera.transform.position += CameraMove;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("SetUp")]
    public GameObject playerFakeBody;
    public GameObject PlayerObject;
    public Rigidbody2D PlayerRigidBody;
    public InputActionReference MovementKeys;
    public FaceMouse combat;

    [Header("Movement")]
    public Vector2 vector2;
    public float CurrentSpeedX;
    public float CurrentSpeedY;
    public Vector2 Velo = Vector2.zero;
    public int SpeedMax = 5;
    public int SpeedStart = 2;
    public int ChangeSpeed = 1;

    //[SerializeField] private float LastX = 0;
    //[SerializeField] private float LastY = 0;

    private void Start()
    {
        CurrentSpeedX = SpeedStart;
        CurrentSpeedY = SpeedStart;
    }

    // Update is called once per frame
    void Update()
    {
        vector2 = MovementKeys.action.ReadValue<Vector2>();

        // Dominant axis
        if (vector2.x != 0f && vector2.y != 0f)
        {
            if (Mathf.Abs(vector2.x) > Mathf.Abs(vector2.y))
            {
                vector2.y = 0f;
            }
            else
            {
                vector2.x = 0f;
            }
        }

        // It's too jammy!!!
        /*
        float XSpeed = vector2.x;
        float YSpeed = vector2.y;
        if (XSpeed == -LastX || (XSpeed == 0 && LastX != 0))
        {
            CurrentSpeedX = SpeedStart;
        }
        if (YSpeed == -LastY || (YSpeed == 0 && LastY !=0))
        {
            CurrentSpeedY = SpeedStart;
        }
        LastY = YSpeed;
        LastX = XSpeed;
        */
    }

    private void FixedUpdate()
    {
        Velo = new Vector2(vector2.x * CurrentSpeedX, vector2.y * CurrentSpeedY);
        PlayerRigidBody.AddForce(Velo);
        if (CurrentSpeedX < SpeedMax)
        {
            CurrentSpeedX += ChangeSpeed * Time.deltaTime;
            if (CurrentSpeedX > SpeedMax)
            {
                CurrentSpeedX = SpeedMax;
            }
        }
        if (CurrentSpeedY < SpeedMax)
        {
            CurrentSpeedY += ChangeSpeed * Time.deltaTime;
            if (CurrentSpeedY > SpeedMax)
            {
                CurrentSpeedY = SpeedMax;
            }
        }
    }
}

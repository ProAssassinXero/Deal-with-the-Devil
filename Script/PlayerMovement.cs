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
    [SerializeField] public Vector2 vector2;
    [SerializeField] public float CurrentSpeedX;
    [SerializeField] public float CurrentSpeedY;
    private Animator playerAnim;
    [SerializeField] private Vector2 Velo = new Vector2(0, 0);
    public int SpeedMax = 5;
    public int SpeedStart = 2;
    public int ChangeSpeed = 1;

    //[SerializeField] private float LastX = 0;
    //[SerializeField] private float LastY = 0;

    private void Start()
    {
        CurrentSpeedX = SpeedStart;
        CurrentSpeedY = SpeedStart;
        playerAnim = PlayerObject.GetComponent<Animator>();
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

        // Probably will use this for combat, i don't like it in the bar. It's too jammy, out of place, and doesn't feel good. But it works, so maybe we'll use it in the future.
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

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("SetUp")]
    public GameObject PlayerObject;
    public Rigidbody2D PlayerRigidBody;
    public InputActionReference MovementKeys;

    [Header("Movement")]
    [SerializeField] private Vector2 vector2;
    [SerializeField] private float CurrentSpeedX;
    [SerializeField] private float CurrentSpeedY;
    [SerializeField] private Vector2 Velo = new Vector2(0,0);
    public int SpeedMax = 5;
    public int SpeedStart = 1;
    public int ChangeSpeed = 1;

    [SerializeField] private float LastX = 0;
    [SerializeField] private float LastY = 0;

    // Update is called once per frame
    void Update()
    {
        vector2 = MovementKeys.action.ReadValue<Vector2>();
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

//Asset intergation for the animation
        //Down
        if (vector2.y < 0)
        {
            PlayerObject.GetComponent<Animator>().SetBool("IsWalkingDown", true);
        }
        else
        {
            PlayerObject.GetComponent<Animator>().SetBool("IsWalkingDown", false);
        }

        //Up
        if (vector2.y > 0)
        {
            PlayerObject.GetComponent<Animator>().SetBool("IsWalkingUp", true);
        }
        else
        {
            PlayerObject.GetComponent<Animator>().SetBool("IsWalkingUp", false);
        }

        //Right
        if (vector2.x > 0)
        {
            PlayerObject.GetComponent<Animator>().SetBool("IsWalkingRight", true);
        }
        else
        {
            PlayerObject.GetComponent<Animator>().SetBool("IsWalkingRight", false);
        }

        //Left
        if (vector2.x < 0)
        {
            PlayerObject.GetComponent<Animator>().SetBool("IsWalkingLeft", true);
        }
        else
        {
            PlayerObject.GetComponent<Animator>().SetBool("IsWalkingLeft", false);
        }
    }
}

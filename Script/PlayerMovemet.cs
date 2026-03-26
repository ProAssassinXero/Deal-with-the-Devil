using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovemet : MonoBehaviour
{
    [Header("SetUp")]
    public GameObject PlayerObject;
    public Rigidbody2D PlayerRigidBody;
    public InputActionReference MovementKeys;

    [Header("Movement")]
    [SerializeField] private Vector2 vector2;
    [SerializeField] private Vector2 LastVect2;
    [SerializeField] private float CurrentSpeed;
    public int SpeedMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vector2 = MovementKeys.action.ReadValue<Vector2>();
        CurrentSpeed = PlayerRigidBody.linearVelocity.magnitude;
    }
    
    void Acc()
    {

    }

    private void FixedUpdate()
    {
        
        PlayerRigidBody.linearVelocity = vector2;
    }
}

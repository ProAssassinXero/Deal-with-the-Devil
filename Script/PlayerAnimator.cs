using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // References
    public PlayerMovement movement;
    public Binbagging binbagging;
    public GameObject playerFakeBody;
    public Animator playerAnim;

    // State
    public Vector2 lastDirection;
    public bool isCombatPhase = false;
    public bool IsSlashing = false;
    public bool isDragging = false;
    public bool canDrag = false;

    // Mouse direction flags
    public bool mouseDirectionDown;
    public bool mouseDirectionUp;
    public bool mouseDirectionRight;
    public bool mouseDirectionLeft;

    // Animator hashes
    public readonly int IsWalkingDown = Animator.StringToHash("IsWalkingDown");
    public readonly int IsWalkingUp = Animator.StringToHash("IsWalkingUp");
    public readonly int IsWalkingRight = Animator.StringToHash("IsWalkingRight");
    public readonly int IsWalkingLeft = Animator.StringToHash("IsWalkingLeft");

    public readonly int IsIdleDown = Animator.StringToHash("IsIdleDown");
    public readonly int IsIdleUp = Animator.StringToHash("IsIdleUp");
    public readonly int IsIdleRight = Animator.StringToHash("IsIdleRight");
    public readonly int IsIdleLeft = Animator.StringToHash("IsIdleLeft");

    public readonly int IsSlashingDown = Animator.StringToHash("IsSlashingDown");
    public readonly int IsSlashingUp = Animator.StringToHash("IsSlashingUp");
    public readonly int IsSlashingRight = Animator.StringToHash("IsSlashingRight");
    public readonly int IsSlashingLeft = Animator.StringToHash("IsSlashingLeft");

    public readonly int IsDraggingDown = Animator.StringToHash("IsDraggingDown");
    public readonly int IsDraggingUp = Animator.StringToHash("IsDraggingUp");
    public readonly int IsDraggingRight = Animator.StringToHash("IsDraggingRight");
    public readonly int IsDraggingLeft = Animator.StringToHash("IsDraggingLeft");

    // Runs once at start
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Runs every frame
    void Update()
    {
        PlayerMouseDirection();   // update mouse direction first
        CheckSlashFinished();     // check if slash ended

        SlashAnimations();        // combat
        BinDragAnimation();       // dragging
        MovementAnimations();     // walking
        IdleAnimations();         // idle (last so it doesn't override others)
    }

    // Gets mouse direction based on rotation
    public void PlayerMouseDirection()
    {
        float z = playerFakeBody.transform.rotation.eulerAngles.z;

        mouseDirectionDown = z < 225 && z > 135;
        mouseDirectionUp = z > 315 || z < 45;
        mouseDirectionRight = z < 315 && z > 225;
        mouseDirectionLeft = z < 135 && z > 45;
    }

    // Handles walking animations
    void MovementAnimations()
    {
        Vector2 vel = movement.vector2;
        bool restrictions = !IsSlashing && !isDragging;

        playerAnim.SetBool(IsWalkingDown, vel.y < 0 && restrictions);
        playerAnim.SetBool(IsWalkingUp, vel.y > 0 && restrictions);
        playerAnim.SetBool(IsWalkingRight, vel.x > 0 && restrictions);
        playerAnim.SetBool(IsWalkingLeft, vel.x < 0 && restrictions);
    }

    // Handles idle animations
    void IdleAnimations()
    {
        bool restrictions = !IsSlashing && !isDragging;
        bool isIdle = movement.vector2 == Vector2.zero;

        playerAnim.SetBool(IsIdleDown, mouseDirectionDown && isIdle && restrictions);
        playerAnim.SetBool(IsIdleUp, mouseDirectionUp && isIdle && restrictions);
        playerAnim.SetBool(IsIdleRight, mouseDirectionRight && isIdle && restrictions);
        playerAnim.SetBool(IsIdleLeft, mouseDirectionLeft && isIdle && restrictions);
    }

    // Handles slash input and animation
    void SlashAnimations()
    {
        bool canSlash = !isDragging && !IsSlashing && isCombatPhase;
        bool mouseDown = Input.GetMouseButtonDown(0);

        if (canSlash && mouseDown)
        {
            if (mouseDirectionDown) SetSlash("IsSlashingDown");
            else if (mouseDirectionUp) SetSlash("IsSlashingUp");
            else if (mouseDirectionRight) SetSlash("IsSlashingRight");
            else if (mouseDirectionLeft) SetSlash("IsSlashingLeft");
        }

        // While slashing, stop movement animations
        if (IsSlashing)
        {
            movement.CurrentSpeedX = 4.5f;
            movement.CurrentSpeedY = 4.5f;

            playerAnim.SetBool(IsWalkingDown, false);
            playerAnim.SetBool(IsWalkingUp, false);
            playerAnim.SetBool(IsWalkingRight, false);
            playerAnim.SetBool(IsWalkingLeft, false);
        }
    }

    // Starts a slash
    void SetSlash(string paramName)
    {
        playerAnim.SetBool(paramName, true);
        IsSlashing = true;
    }

    // Stops all slash animations
    public void StopSlash()
    {
        playerAnim.SetBool("IsSlashingDown", false);
        playerAnim.SetBool("IsSlashingUp", false);
        playerAnim.SetBool("IsSlashingRight", false);
        playerAnim.SetBool("IsSlashingLeft", false);

        IsSlashing = false;
    }

    // Checks if slash animation finished
    void CheckSlashFinished()
    {
        AnimatorStateInfo stateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);

        if ((stateInfo.IsName("The Client-Down Slash") ||
             stateInfo.IsName("The Client-Up Slash") ||
             stateInfo.IsName("The Client-Right Slash") ||
             stateInfo.IsName("The Client-Left Slash"))
             && stateInfo.normalizedTime >= 1)
        {
            StopSlash();
        }
    }

    // Handles dragging logic + animations
    void BinDragAnimation()
    {
        // Store last movement direction
        if (movement.Velo != Vector2.zero)
            lastDirection = movement.Velo;

        // Pause animation if standing still while dragging
        playerAnim.speed = (movement.Velo == Vector2.zero && isDragging) ? 0 : 1;

        // Can we drag?
        canDrag = !IsSlashing && !isCombatPhase && binbagging.BodyCount == 1;

        // Start dragging
        if (canDrag && !isDragging)
            isDragging = true;

        // Stop dragging
        if (binbagging.BodyCount == 0)
            isDragging = false;

        // Drag animations
        playerAnim.SetBool(IsDraggingDown, canDrag && (movement.vector2.y < 0 || lastDirection.y < 0));
        playerAnim.SetBool(IsDraggingUp, canDrag && (movement.vector2.y > 0 || lastDirection.y > 0));
        playerAnim.SetBool(IsDraggingRight, canDrag && (movement.vector2.x > 0 || lastDirection.x > 0));
        playerAnim.SetBool(IsDraggingLeft, canDrag && (movement.vector2.x < 0 || lastDirection.x < 0));
    }
}
using UnityEngine;

public class PlayerAnimator : PhaseManager
{
    // References
    private PlayerMovement movement;
    private Binbagging binbagging;
    public GameObject playerFakeBody;
    public Animator playerAnim;

    // State
    public Vector2 lastDirection;

    public PlayerPhases PhaseManager;

    public bool IsSlashing = false;
    public bool isDragging = false;
    public bool canDrag = false;

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
        movement = GetComponent<PlayerMovement>();
        binbagging = GetComponent<Binbagging>();
    }

    // Runs every frame
    void Update()
    {
        CheckSlashFinished();     // check if slash ended

        SlashAnimations();        // combat
        BinDragAnimation();       // dragging
        MovementAnimations();     // walking
        IdleAnimations();         // idle (last so it doesn't override others)
    }

    // Gets mouse direction based on rotation

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

        playerAnim.SetBool(IsIdleDown, (movement.vector2.y < 0 || lastDirection.y < 0) && isIdle && restrictions);
        playerAnim.SetBool(IsIdleUp, (movement.vector2.y > 0 || lastDirection.y > 0) && isIdle && restrictions);
        playerAnim.SetBool(IsIdleRight, (movement.vector2.x > 0 || lastDirection.x > 0) && isIdle && restrictions);
        playerAnim.SetBool(IsIdleLeft, (movement.vector2.x < 0 || lastDirection.x < 0) && isIdle && restrictions);
    }

    // Handles slash input and animation
    void SlashAnimations()
    {
        bool canSlash = !isDragging && !IsSlashing && PhaseManager.CurrentPhase == Phases.Combat;
        bool mouseDown = Input.GetMouseButtonDown(0);

        if (canSlash && mouseDown)
        {
            if (movement.vector2.y < 0 || lastDirection.y < 0) SetSlash("IsSlashingDown");
            else if (movement.vector2.y > 0 || lastDirection.y > 0) SetSlash("IsSlashingUp");
            else if (movement.vector2.x > 0 || lastDirection.x > 0) SetSlash("IsSlashingRight");
            else if (movement.vector2.x < 0 || lastDirection.x < 0) SetSlash("IsSlashingLeft");
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

    private bool dragStart = false;
    // Handles dragging logic + animations
    void BinDragAnimation()
    {
        // Store last movement direction
        if (movement.Velo != Vector2.zero)
            lastDirection = movement.Velo;

        // Pause animation if standing still while dragging
        playerAnim.speed = (movement.Velo == Vector2.zero && isDragging) ? 0 : 1;

        // Can we drag?
        canDrag = !IsSlashing && PhaseManager.CurrentPhase == Phases.Clean_Up && binbagging.BodyCount == 1;

        if (dragStart && playerAnim.speed == 1)
        {
            dragStart = false;
        }

        // Start dragging
        if (canDrag && !isDragging)
        {
            dragStart = true;
            isDragging = true;
        }
            
            

        // Stop dragging
        if (binbagging.BodyCount == 0)
        {
            isDragging = false;
        }
            
        
        // Drag animations

        if (dragStart)
        {
            playerAnim.SetBool(IsDraggingDown, canDrag && (movement.vector2.y > 0 || lastDirection.y > 0));
            playerAnim.SetBool(IsDraggingUp, canDrag && (movement.vector2.y < 0 || lastDirection.y < 0));
            playerAnim.SetBool(IsDraggingRight, canDrag && (movement.vector2.x < 0 || lastDirection.x < 0));
            playerAnim.SetBool(IsDraggingLeft, canDrag && (movement.vector2.x > 0 || lastDirection.x > 0));
            
        }
        else
        {
            playerAnim.SetBool(IsDraggingDown, canDrag && (movement.vector2.y < 0 || lastDirection.y < 0));
            playerAnim.SetBool(IsDraggingUp, canDrag && (movement.vector2.y > 0 || lastDirection.y > 0));
            playerAnim.SetBool(IsDraggingRight, canDrag && (movement.vector2.x > 0 || lastDirection.x > 0));
            playerAnim.SetBool(IsDraggingLeft, canDrag && (movement.vector2.x < 0 || lastDirection.x < 0));
        }
        
    }
}
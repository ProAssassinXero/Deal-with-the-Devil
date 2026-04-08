using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Vector2 lastDirection;
    public PlayerMovement movement;
    public Binbagging binbagging;
    public GameObject playerFakeBody;
    public bool isComatPhase = false;
    public bool IsSlashing = false;
    public bool isDragging = false;
    public bool canDrag = false;

    public Animator playerAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSlashFinished();
        SlashAnimations();
        IdleAnimations();
        MovementAnimations();
        BinDragAnimation();
    }

    void MovementAnimations()
    {
        // Asset integration for the animation
        //Movement Animations---------------------------------------------------------------------------------------------------------------------------------
        // Down
        if (movement.vector2.y < 0 && IsSlashing == false && isDragging == false)
        {
            playerAnim.SetBool("IsWalkingDown", true);
        }
        else
        {
            playerAnim.SetBool("IsWalkingDown", false);
        }

        // Up
        if (movement.vector2.y > 0 && IsSlashing == false && isDragging == false)
        {
            playerAnim.SetBool("IsWalkingUp", true);
        }
        else
        {
            playerAnim.SetBool("IsWalkingUp", false);
        }

        // Right
        if (movement.vector2.x > 0 && IsSlashing == false && isDragging == false)
        {
            playerAnim.SetBool("IsWalkingRight", true);
        }
        else
        {
            playerAnim.SetBool("IsWalkingRight", false);
        }

        // Left
        if (movement.vector2.x < 0 && IsSlashing == false && isDragging == false)
        {
            playerAnim.SetBool("IsWalkingLeft", true);
        }
        else
        {
            playerAnim.SetBool("IsWalkingLeft", false);
        }
    }

    void IdleAnimations()
    {
        //Idle Animations---------------------------------------------------------------------------------------------------------------------------------
        //Down Idle
        if (playerFakeBody.transform.rotation.eulerAngles.z < 225 && playerFakeBody.transform.rotation.eulerAngles.z > 135 && movement.vector2.y == 0 && movement.vector2.x == 0 && IsSlashing == false && isDragging == false)
        {
            playerAnim.SetBool("IsIdleDown", true);
        }
        else
        {
            playerAnim.SetBool("IsIdleDown", false);
        }
        //Up Idle
        if ((playerFakeBody.transform.rotation.eulerAngles.z > 315 || playerFakeBody.transform.rotation.eulerAngles.z < 45) && movement.vector2.y == 0 && movement.vector2.x == 0 && IsSlashing == false && isDragging == false)
        {
            playerAnim.SetBool("IsIdleUp", true);
        }
        else
        {
            playerAnim.SetBool("IsIdleUp", false);
        }


        //Right Idle
        if (playerFakeBody.transform.rotation.eulerAngles.z < 315 && playerFakeBody.transform.rotation.eulerAngles.z > 225 && movement.vector2.y == 0 && movement.vector2.x == 0 && IsSlashing == false && isDragging == false)
        {
            playerAnim.SetBool("IsIdleRight", true);
        }
        else
        {
            playerAnim.SetBool("IsIdleRight", false);
        }
        //Left Idle
        if (playerFakeBody.transform.rotation.eulerAngles.z < 135 && playerFakeBody.transform.rotation.eulerAngles.z > 45 && movement.vector2.y == 0 && movement.vector2.x == 0 && IsSlashing == false && isDragging == false)
        {
            playerAnim.SetBool("IsIdleLeft", true);
        }
        else
        {
            playerAnim.SetBool("IsIdleLeft", false);
        }
    }

    void SlashAnimations()
    {
        //Slash Animations---------------------------------------------------------------------------------------------------------------------------------
        //Down Slash
        if ((playerFakeBody.transform.rotation.eulerAngles.z < 225 && playerFakeBody.transform.rotation.eulerAngles.z > 135) && Input.GetMouseButtonDown(0) && IsSlashing == false && isComatPhase == true)
        {
            playerAnim.SetBool("IsSlashingDown", true);
            IsSlashing = true;
        }
        //Up Slash
        if ((playerFakeBody.transform.rotation.eulerAngles.z > 315 || playerFakeBody.transform.rotation.eulerAngles.z < 45) && Input.GetMouseButtonDown(0) && IsSlashing == false && isComatPhase == true)
        {
            playerAnim.SetBool("IsSlashingUp", true);
            IsSlashing = true;
        }

        //Right Slash
        if (playerFakeBody.transform.rotation.eulerAngles.z < 315 && playerFakeBody.transform.rotation.eulerAngles.z > 225 && Input.GetMouseButtonDown(0) && IsSlashing == false && isComatPhase == true)
        {
            playerAnim.SetBool("IsSlashingRight", true);
            IsSlashing = true;
        }
        //Left Slash
        if (playerFakeBody.transform.rotation.eulerAngles.z < 135 && playerFakeBody.transform.rotation.eulerAngles.z > 45 && Input.GetMouseButtonDown(0) && IsSlashing == false && isComatPhase == true)
        {
            playerAnim.SetBool("IsSlashingLeft", true);
            IsSlashing = true;
        }

        //combat reference for the animation
        if (IsSlashing)
        {
            movement.CurrentSpeedX = 4.5f;
            movement.CurrentSpeedY = 4.5f;
            playerAnim.SetBool("IsWalkingDown", false);
            playerAnim.SetBool("IsWalkingUp", false);
            playerAnim.SetBool("IsWalkingRight", false);
            playerAnim.SetBool("IsWalkingLeft", false);
        }
    }

    public void StopSlash()
    {
        playerAnim.SetBool("IsSlashingDown", false);
        playerAnim.SetBool("IsSlashingUp", false);
        playerAnim.SetBool("IsSlashingRight", false);
        playerAnim.SetBool("IsSlashingLeft", false);
        IsSlashing = false;
    }

    void CheckSlashFinished()
    {
        if (!IsSlashing) return;

        AnimatorStateInfo stateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("The Client-Down Slash") && stateInfo.normalizedTime >= 1)
        {
            StopSlash();
        }

        if (stateInfo.IsName("The Client-Up Slash") && stateInfo.normalizedTime >= 1)
        {
            StopSlash();
        }

        if (stateInfo.IsName("The Client-Right Slash") && stateInfo.normalizedTime >= 1)
        {
            StopSlash();
        }
        if (stateInfo.IsName("The Client-Left Slash") && stateInfo.normalizedTime >= 1)
        {
            StopSlash();
        }
    }

    void BinDragAnimation()
    {
        // Last Direction for Dragging Animations
        if (movement.Velo != Vector2.zero)
        {
            lastDirection = movement.Velo;
        }
        if (binbagging.BodyCount == 0)
        {
            isDragging = false;
        }

        //Drag Animations---------------------------------------------------------------------------------------------------------------------------------
        canDrag = !IsSlashing && !isComatPhase && binbagging.BodyCount == 1;

        // Down Drag
        if (canDrag && (movement.vector2.y < 0 || lastDirection.y < 0))
        {
            playerAnim.SetBool("IsDraggingDown", true);
            isDragging = true;
        }
        else
        {
            playerAnim.SetBool("IsDraggingDown", false);
        }

        // Up Drag
        if (canDrag && (movement.vector2.y > 0 || lastDirection.y > 0))
        {
            playerAnim.SetBool("IsDraggingUp", true);
            isDragging = true;
        }
        else
        {
            playerAnim.SetBool("IsDraggingUp", false);
        }

        // Right Drag
        if (canDrag && (movement.vector2.x > 0 || lastDirection.x > 0))
        {
            playerAnim.SetBool("IsDraggingRight", true);
            isDragging = true;
        }
        else
        {
            playerAnim.SetBool("IsDraggingRight", false);
        }

        // Left Drag
        if (canDrag && (movement.vector2.x < 0 || lastDirection.x < 0))
        {
            playerAnim.SetBool("IsDraggingLeft", true);
            isDragging = true;
        }
        else
        {
            playerAnim.SetBool("IsDraggingLeft", false);
        }
    }
}

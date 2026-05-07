using UnityEngine;

public class AI_Animation : MonoBehaviour
{
    public Animator aiAnimator;
    public AIMovement aiMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (aiAnimator == null)
        {
            aiAnimator = GetComponent<Animator>();
            aiMovement = GetComponent<AIMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Seating();
    }

    void Walk()
    {
        if (aiMovement.movementDirection.x > 0)
        {
            aiAnimator.SetBool("IsWalkingRight", true);
        }
        else
        {
            aiAnimator.SetBool("IsWalkingRight", false);
        }

        if (aiMovement.movementDirection.x < 0)
        {
            aiAnimator.SetBool("IsWalkingLeft", true);
        }
        else
        {
            aiAnimator.SetBool("IsWalkingLeft", false);
        }

        if (aiMovement.movementDirection.y > 0)
        {
            aiAnimator.SetBool("IsWalkingUp", true);
        }
        else
        {
            aiAnimator.SetBool("IsWalkingUp", false);
        }

        if (aiMovement.movementDirection.y < 0)
        {
            aiAnimator.SetBool("IsWalkingDown", true);
        }
        else
        {
            aiAnimator.SetBool("IsWalkingDown", false);
        }
    }
    void Seating()
    {

        //up
        if (aiMovement.sitUp && aiMovement.movementDirection == Vector2.zero)
        {
            aiAnimator.SetBool("IsSittingUp", true);
        }
        else
        {
            aiAnimator.SetBool("IsSittingUp", false);
        }

        //down
        if (aiMovement.sitDown && aiMovement.movementDirection == Vector2.zero)
        {
            aiAnimator.SetBool("IsSittingDown", true);
        }
        else
        {
            aiAnimator.SetBool("IsSittingDown", false);
        }

        //left
        if (aiMovement.sitLeft && aiMovement.movementDirection == Vector2.zero)
        {
            aiAnimator.SetBool("IsSittingLeft", true);
        }
        else
        {
            aiAnimator.SetBool("IsSittingLeft", false);
        }

        if (aiMovement.sitRight && aiMovement.movementDirection == Vector2.zero)
        {
            aiAnimator.SetBool("IsSittingRight", true);
        }
        else
        {
            aiAnimator.SetBool("IsSittingRight", false);
        }            
    }
}

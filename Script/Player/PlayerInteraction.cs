using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //public SpriteRenderer topSprite;
    //public SpriteRenderer leftSprite;
    //public SpriteRenderer rightSprite;
    //public SpriteRenderer bottomSprite;

    public BoxCollider2D topCollider;
    public BoxCollider2D leftCollider;
    public BoxCollider2D rightCollider;
    public BoxCollider2D bottomCollider;

    

    public PlayerAnimator PlayerAnimator;

    // Update is called once per frame
    void Update()
    {
        //Interaction Hitbox
        // Down
        if (PlayerAnimator.playerAnim.GetBool("IsWalkingDown") == true || PlayerAnimator.playerAnim.GetBool("IsIdleDown") == true || PlayerAnimator.playerAnim.GetBool("IsDraggingDown") || PlayerAnimator.playerAnim.GetBool("IsSlashingDown"))
        {
            //bottomSprite.enabled = true;
            bottomCollider.enabled = true;

        }
        else
        {
            //bottomSprite.enabled = false;
            bottomCollider.enabled = false;

        }

        // Up
        if (PlayerAnimator.playerAnim.GetBool("IsWalkingUp") == true || PlayerAnimator.playerAnim.GetBool("IsIdleUp") == true || PlayerAnimator.playerAnim.GetBool("IsDraggingUp") || PlayerAnimator.playerAnim.GetBool("IsSlashingUp"))
        {
            //topSprite.enabled = true;
            topCollider.enabled = true;
        }
        else
        {
            //topSprite.enabled = false;
            topCollider.enabled = false;
        }

        // Left
        if (PlayerAnimator.playerAnim.GetBool("IsWalkingLeft") == true || PlayerAnimator.playerAnim.GetBool("IsIdleLeft") == true ||  PlayerAnimator.playerAnim.GetBool("IsDraggingLeft") || PlayerAnimator.playerAnim.GetBool("IsSlashingLeft"))
        {
            //leftSprite.enabled = true;
            leftCollider.enabled = true;

        }
        else
        {
            //leftSprite.enabled = false;
            leftCollider.enabled = false;

        }

        //Right
        if (PlayerAnimator.playerAnim.GetBool("IsWalkingRight") == true || PlayerAnimator.playerAnim.GetBool("IsIdleRight") == true || PlayerAnimator.playerAnim.GetBool("IsDraggingRight") || PlayerAnimator.playerAnim.GetBool("IsSlashingRight"))
        {
            //rightSprite.enabled = true;
            rightCollider.enabled = true;
        }
        else
        {
            //rightSprite.enabled = false;
            rightCollider.enabled = false;
        }
    }
}

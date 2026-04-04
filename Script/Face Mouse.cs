using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    public bool IsSlashing = false;
    public GameObject PlayerObject;
    private Animator playerAnim;

    void Start()
    {
        playerAnim = PlayerObject.GetComponent<Animator>();
    }
    void Update()
    {
        CheckSlashFinished();
        FaceTheMouse();
    }

    void FaceTheMouse()
    {
        //Reference
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Calculation
        Vector2 direction = new Vector2(mousePosition.x - gameObject.transform.position.x, mousePosition.y - gameObject.transform.position.y);
        transform.up = direction;

        //Slash Animation
        //Down Slash
        if ((gameObject.transform.rotation.eulerAngles.z < 225 && gameObject.transform.rotation.eulerAngles.z > 135) && Input.GetMouseButtonDown(0) && IsSlashing == false)
        {
            playerAnim.SetBool("IsSlashingDown", true);
            IsSlashing = true;
        }
        //Up Slash
        if ((gameObject.transform.rotation.eulerAngles.z > 315 || gameObject.transform.rotation.eulerAngles.z < 45) && Input.GetMouseButtonDown(0) && IsSlashing == false)
        {
            playerAnim.SetBool("IsSlashingUp", true);
            IsSlashing = true;
        }

        //Right Slash
        if (gameObject.transform.rotation.eulerAngles.z < 315 && gameObject.transform.rotation.eulerAngles.z > 225 && Input.GetMouseButtonDown(0) && IsSlashing == false)
        {
            playerAnim.SetBool("IsSlashingRight", true);
            IsSlashing = true;
        }
        //Left Slash
        if (gameObject.transform.rotation.eulerAngles.z > 45 && gameObject.transform.rotation.eulerAngles.z < 135 && Input.GetMouseButtonDown(0) && IsSlashing == false)
        {
            playerAnim.SetBool("IsSlashingLeft", true);
            IsSlashing = true;
        }


        //Reset Slash Animation (Debug)
        if (Input.GetKeyDown(KeyCode.R))
        {
            IsSlashing = false;
            playerAnim.SetBool("IsSlashingDown", false);
            playerAnim.SetBool("IsSlashingUp", false);
            playerAnim.SetBool("IsSlashingRight", false);
            playerAnim.SetBool("IsSlashingLeft", false);
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

}

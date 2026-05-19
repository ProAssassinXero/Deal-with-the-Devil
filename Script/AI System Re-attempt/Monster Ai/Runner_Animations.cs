using UnityEngine;

public class Runner_Animations : MonoBehaviour
{
    public RunnerScript runnerScript;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // RunRight: moving right AND horizontal > vertical
        if (runnerScript.movementDirection.x > 0 && runnerScript.movementDirection.x > runnerScript.movementDirection.y)
        {
            animator.SetBool("RunRight", true);
        }
        else
        {
            animator.SetBool("RunRight", false);
        }

        // RunLeft: moving left AND horizontal magnitude > vertical
        if (runnerScript.movementDirection.x < 0 && runnerScript.movementDirection.x < runnerScript.movementDirection.y)
        {
            animator.SetBool("RunLeft", true);
        }
        else
        {
            animator.SetBool("RunLeft", false);
        }

        // RunUp: moving up AND vertical > horizontal
        if (runnerScript.movementDirection.y > 0 && runnerScript.movementDirection.y > runnerScript.movementDirection.x)
        {
            animator.SetBool("RunUp", true);
        }
        else
        {
            animator.SetBool("RunUp", false);
        }

        // RunDown: moving down AND vertical magnitude > horizontal
        if (runnerScript.movementDirection.y < 0 && runnerScript.movementDirection.y < runnerScript.movementDirection.x)
        {
            animator.SetBool("RunDown", true);
        }
        else
        {
            animator.SetBool("RunDown", false);
        }
    }
}

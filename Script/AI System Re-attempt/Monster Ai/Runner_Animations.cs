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
        if (runnerScript.movementDirection.x == 1)
        {
            animator.SetFloat("Xaxis", 1);
        }
        if (runnerScript.movementDirection.x == -1)
        {
            animator.SetFloat("Xaxis", -1);
        }
        if (runnerScript.movementDirection.y == 1)
        {
            animator.SetFloat("Yaxis", 1);
        }
        if (runnerScript.movementDirection.y == -1)
        {
            animator.SetFloat("Yaxis", -1);
        }
    }
}

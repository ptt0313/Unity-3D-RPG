using UnityEngine;

public class RunningStateBehaviour : StateMachineBehaviour
{
    bool isMove;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enter Running State");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isMove = animator.GetBool("Move");
        if(!isMove)
        {
            animator.SetTrigger("Idle");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Jump");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetTrigger("Dodge");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exit Running State");
    }
}

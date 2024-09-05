using UnityEngine;

public class IdleStateBehaviour : StateMachineBehaviour
{
    bool isMove;
    bool isArmed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enter Idle State");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        isMove = animator.GetBool("Move");
        if (isMove)
        {
            animator.SetTrigger("Run");
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Jump");
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Dodge");
        }
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Armed", true);
            animator.SetTrigger("Draw");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exit Idle State");
    }
}

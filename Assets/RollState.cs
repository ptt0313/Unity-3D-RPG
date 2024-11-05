using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : StateMachineBehaviour
{
    public float RollDistance = 3;  // ĳ���Ͱ� �ڷ� �̵��� �Ÿ�
    public float moveDuration = 1f;      // �̵� ���� �ð�

    private Vector3 startPosition;       // �̵� ���� ��ġ
    private Vector3 endPosition;         // �̵� �� ��ġ
    private float moveStartTime;         // �̵� ���� �ð�
    private bool isMoving = false;       // �̵� �� ����

    // �ִϸ��̼� ���¿� ������ �� ȣ��
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �̵� ���� �ð� ���
        moveStartTime = Time.time;

        // �̵� ���� ��ġ ����
        startPosition = animator.transform.position;

        endPosition = startPosition + animator.transform.forward * RollDistance;

        // �̵� ���� �÷��� ����
        isMoving = true;
    }

    // �ִϸ��̼��� ���� ���� �� ȣ�� (�����Ӹ��� ����)
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isMoving)
        {
            // ���� �ð����� ����� �ð� ���
            float elapsed = Time.time - moveStartTime;
            float t = elapsed / moveDuration;

            // t ���� 1�� ���� ������ �̵��� �����
            if (t <= 1f)
            {
                // ������Ʈ�� ���� ����(Lerp)�� ���� �ڷ� �̵���Ŵ
                Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, t);
                currentPosition.y = startPosition.y;  // Y���� ������ ���� (���� ���� �ڷθ� �̵�)

                // ���ο� ��ġ�� Animator�� Transform�� ����
                animator.transform.position = currentPosition;
            }
            else
            {
                // �̵� ����
                isMoving = false;
            }
        }
    }

    // �ִϸ��̼� ���¿��� ���� �� ȣ�� (optional)
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���¿��� ������ �̵� ����
        isMoving = false;

    }
}

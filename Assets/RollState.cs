using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : StateMachineBehaviour
{
    public float RollDistance = 3;  // 캐릭터가 뒤로 이동할 거리
    public float moveDuration = 1f;      // 이동 지속 시간

    private Vector3 startPosition;       // 이동 시작 위치
    private Vector3 endPosition;         // 이동 끝 위치
    private float moveStartTime;         // 이동 시작 시간
    private bool isMoving = false;       // 이동 중 여부

    // 애니메이션 상태에 진입할 때 호출
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 이동 시작 시간 기록
        moveStartTime = Time.time;

        // 이동 시작 위치 설정
        startPosition = animator.transform.position;

        endPosition = startPosition + animator.transform.forward * RollDistance;

        // 이동 시작 플래그 설정
        isMoving = true;
    }

    // 애니메이션이 실행 중일 때 호출 (프레임마다 실행)
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isMoving)
        {
            // 현재 시간에서 경과된 시간 계산
            float elapsed = Time.time - moveStartTime;
            float t = elapsed / moveDuration;

            // t 값이 1을 넘지 않으면 이동을 계속함
            if (t <= 1f)
            {
                // 오브젝트를 선형 보간(Lerp)을 통해 뒤로 이동시킴
                Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, t);
                currentPosition.y = startPosition.y;  // Y값은 변하지 않음 (점프 없이 뒤로만 이동)

                // 새로운 위치를 Animator의 Transform에 적용
                animator.transform.position = currentPosition;
            }
            else
            {
                // 이동 종료
                isMoving = false;
            }
        }
    }

    // 애니메이션 상태에서 나갈 때 호출 (optional)
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 상태에서 나가면 이동 종료
        isMoving = false;

    }
}

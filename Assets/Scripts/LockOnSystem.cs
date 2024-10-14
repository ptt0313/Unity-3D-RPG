using UnityEngine;
using Cinemachine;

public class LockOnSystem : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;  // Cinemachine Virtual Camera
    public Transform player;                        // 플레이어 캐릭터
    public Transform boss;                          // 보스 캐릭터
    public float lockOnDistance = 10f;              // 락온 가능한 거리
    private bool isLockedOn = false;                // 락온 상태를 나타내는 변수

    private float Yaxis;
    private float Xaxis;
    private float rotSensitive = 5f;//카메라 회전 감도
    private float RotationMin = -10f;//카메라 회전각도 최소
    private float RotationMax = 80f;//카메라 회전각도 최대
    private float smoothTime = 0.12f;//카메라가 회전하는데 걸리는 시간
    void Start()
    {
        // 기본적으로 플레이어를 바라보도록 설정
        virtualCamera.Follow = player;
        virtualCamera.LookAt = player;
    }

    void Update()
    {
        // 락온 토글 (L키 입력 시 락온/해제)
        if (Input.GetMouseButtonDown(2))
        {
            if (isLockedOn)
            {
                // 락온 해제
                UnlockTarget();
            }
            else if (!isLockedOn)
            {
                // 플레이어와 보스 사이의 거리가 락온 가능 거리 내에 있으면 락온
                LockOnTarget(boss);
            }
        }
    }
    // 타겟을 락온
    void LockOnTarget(Transform target)
    {
        isLockedOn = true;
        virtualCamera.LookAt = target;  // 보스를 바라보게 설정
    }

    // 락온 해제
    void UnlockTarget()
    {
        isLockedOn = false;
        virtualCamera.LookAt = player;  // 플레이어를 바라보게 설정
    }
}

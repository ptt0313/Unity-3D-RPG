using UnityEngine;
using Cinemachine;

public class LockOnSystem : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;  // Cinemachine Virtual Camera
    public Transform player;                        // �÷��̾� ĳ����
    public Transform boss;                          // ���� ĳ����
    public float lockOnDistance = 10f;              // ���� ������ �Ÿ�
    private bool isLockedOn = false;                // ���� ���¸� ��Ÿ���� ����

    private float Yaxis;
    private float Xaxis;
    private float rotSensitive = 5f;//ī�޶� ȸ�� ����
    private float RotationMin = -10f;//ī�޶� ȸ������ �ּ�
    private float RotationMax = 80f;//ī�޶� ȸ������ �ִ�
    private float smoothTime = 0.12f;//ī�޶� ȸ���ϴµ� �ɸ��� �ð�
    void Start()
    {
        // �⺻������ �÷��̾ �ٶ󺸵��� ����
        virtualCamera.Follow = player;
        virtualCamera.LookAt = player;
    }

    void Update()
    {
        // ���� ��� (LŰ �Է� �� ����/����)
        if (Input.GetMouseButtonDown(2))
        {
            if (isLockedOn)
            {
                // ���� ����
                UnlockTarget();
            }
            else if (!isLockedOn)
            {
                // �÷��̾�� ���� ������ �Ÿ��� ���� ���� �Ÿ� ���� ������ ����
                LockOnTarget(boss);
            }
        }
    }
    // Ÿ���� ����
    void LockOnTarget(Transform target)
    {
        isLockedOn = true;
        virtualCamera.LookAt = target;  // ������ �ٶ󺸰� ����
    }

    // ���� ����
    void UnlockTarget()
    {
        isLockedOn = false;
        virtualCamera.LookAt = player;  // �÷��̾ �ٶ󺸰� ����
    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        virtualCamera.LookAt = GameObject.Find("Camera Position").transform;
        virtualCamera.Follow = GameObject.Find("Camera Position").transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using yonguk;

public class PlayerManager : MonoBehaviour
{
    InputHandler inputHandler;
    Animator anim;
    CameraHandler cameraHandler;
    PlayerLocomotion playerLocomotion;

    public bool isInteracting;
    private void Awake()
    {
        cameraHandler = CameraHandler.Instance;
    }
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        anim = GetComponent<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        isInteracting = anim.GetBool("isInteracting");


        float delta = Time.deltaTime;

        inputHandler.TickInput(delta);
        playerLocomotion.HandleMovement(delta);

        playerLocomotion.HandleRollingAndSprinting(delta);

    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }
    }
    private void LateUpdate()
    {
        inputHandler.rollFlag = false;
        inputHandler.rb_Input = false;
    }
}

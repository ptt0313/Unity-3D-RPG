using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]//인스펙터에서만 참조 가능하게
    private float smoothRotationTime;//target 각도로 회전하는데 걸리는 시간
    [SerializeField]
    private float smoothMoveTime;//target 속도로 바뀌는데 걸리는 시간
    private float moveSpeed = 10f;//움직이는 속도
    private float rotationVelocity;//The current velocity, this value is modified by the function every time you call it.
    private float speedVelocity;//The current velocity, this value is modified by the function every time you call it.
    private float currentSpeed;
    private float targetSpeed;
    private Transform cameraTransform;
    private Animator animator;
    public Vector2 input;
    private bool isAction;
    private bool isArmed = false;
    [SerializeField] GameObject[] Weapons;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    private void Move()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        if (inputDir != Vector2.zero)//움직임을 멈췄을 때 다시 처음 각도로 돌아가는걸 막기위함
        {
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref rotationVelocity, smoothRotationTime);
        }
        targetSpeed = moveSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, smoothMoveTime);
        //현재스피드에서 타겟스피드까지 smoothMoveTime 동안 변한다
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        if (input != Vector2.zero)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }
    void Update()
    {
        isArmed = animator.GetBool("Armed");
        isAction = animator.GetBool("Action");
        if (isAction == false)
        {
            Move();
        }
        if(isArmed == true)
        {
            Weapons[1].SetActive(true);
            Weapons[0].SetActive(false);
        }
        else
        {
            Weapons[1].SetActive(false);
            Weapons[0].SetActive(true);
        }
    }
}

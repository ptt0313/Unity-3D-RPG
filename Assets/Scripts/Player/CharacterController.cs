using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float rotationTime;//target 각도로 회전하는데 걸리는 시간
    [SerializeField] private float moveTime;//target 속도로 바뀌는데 걸리는 시간
    [SerializeField] private float moveSpeed = 5f;//움직이는 속도
    private float rotationVelocity; //회전 속도
    private float speedVelocity; // 속도
    private float currentSpeed; // 현재 속도
    private float targetSpeed;
    private Transform cameraTransform;
    private Animator animator;
    public Vector2 input;
    private bool isInteracting;
    private bool isAttacking;
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
        // 캐릭터가 움직인 방향을 바라보도록 설정
        if (inputDir != Vector2.zero)
        {
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref rotationVelocity, rotationTime);
        }

        // targetSpeed 는 속도 * 방향의 크기
        targetSpeed = moveSpeed * inputDir.magnitude;

        //currentSpeed에서 targetSpeed까지 moveTime동안 변환
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, moveTime);


        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        if (input != Vector2.zero)
        {
            animator.SetTrigger("Run");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
        
    }
    private void Attack()
    {
        if (isInteracting == false && Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isInteracting", true);
            animator.SetBool("isAttacking", true);
            animator.Play("Attack");
        }
        else if (isAttacking == true && Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isInteracting", true);
            animator.Play("Attack2");
            animator.SetBool("isAttacking", false);
        }

    }
    void Update()
    {
        isInteracting = animator.GetBool("isInteracting");
        isAttacking = animator.GetBool("isAttacking");
        if (isInteracting == false)
        {
            Move();
            Attack();
        }
    }
}

using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float rotationTime;//target ������ ȸ���ϴµ� �ɸ��� �ð�
    [SerializeField] private float moveTime;//target �ӵ��� �ٲ�µ� �ɸ��� �ð�
    [SerializeField] private float moveSpeed = 5f;//�����̴� �ӵ�
    private float rotationVelocity; //ȸ�� �ӵ�
    private float speedVelocity; // �ӵ�
    private float currentSpeed; // ���� �ӵ�
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
        // ĳ���Ͱ� ������ ������ �ٶ󺸵��� ����
        if (inputDir != Vector2.zero)
        {
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref rotationVelocity, rotationTime);
        }

        // targetSpeed �� �ӵ� * ������ ũ��
        targetSpeed = moveSpeed * inputDir.magnitude;

        //currentSpeed���� targetSpeed���� moveTime���� ��ȯ
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

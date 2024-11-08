using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] BasePlayerState playerState;
    [SerializeField] GameObject playerHitBox;

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
    private bool isDie;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        StartCoroutine(RegenHp());
        StartCoroutine(RegenStamina());
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
            animator.Play("Run");
        }
        
        else
        {
            animator.Play("Idle");
        }

    }
    private void Die()
    {
        if (playerState.hp <= 0 && isDie == false)
        {
            isDie = true;
            animator.Play("Die");
            playerHitBox.SetActive(false);
            Invoke("Respawn", 3f);
        }
    }
    
    private void Attack()
    {
        if (isInteracting == false && Input.GetMouseButtonDown(0))
        {
            animator.Play("Attack");
        }
    }
    private void Roll()
    {
        if(playerState.stamina >= 20 && isInteracting == false && Input.GetKeyDown(KeyCode.Space))
        {
            playerState.stamina -= 20;
            animator.Play("Roll");
        }   
    }
    void Update()
    {
        isInteracting = animator.GetBool("isInteracting");
        if (isInteracting == false)
        {
            Move();
            Attack();
            Roll();
        }
        Die();
        
    }
    void Respawn()
    {
        SceneManagement.Instance.StartLoadScene(0);
        PlayerInfomationManager.Instance.playerState.hp = 1;
        animator.Play("Idle");
        playerHitBox.SetActive (true);
        isDie = false;
    }
    IEnumerator RegenStamina()
    {
        while (true)
        {
            if (isDie == false)
            {
                yield return new WaitForSeconds(1);
                if (playerState.stamina < playerState.maxStamina && isInteracting == false)
                {
                    playerState.stamina += 1;
                }
            }
            else
            {
                yield return null;
            }
        }
    }
    IEnumerator RegenHp()
    {
        while(true)
        {
            if(isDie == false)
            {
                yield return new WaitForSeconds(1);
                if (playerState.hp < playerState.maxHp)
                {
                    playerState.hp += 1;
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}

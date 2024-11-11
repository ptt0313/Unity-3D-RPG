# Unity 3D RPG

## 1. 개발 기간
약 1.5개월

## 2. 개발 환경
1. Unity 2022
2. Visual Studio 2022
---
## 3. 핵심 시스템

### 1. 플레이어 이동
<details><summary>접기/펼치기</summary>
플레이어의 이동은 유니티의 Input System을 사용해서 만들었습니다.
먼저 GetAxisRaw를 사용하여 Horizontal과 Vertical의 값을 Vector2로 가져옵니다.
가져온 Vector2의 값의 벡터를 정규화해준 뒤 입력받은 키값의 방향으로 캐릭터가 바라보게하고
바라본 방향으로 캐릭터가 움직일수 있게 했습니다.

Input의 입력이 없을 경우 캐릭터는 제자리에 서있는 애니메이션을 플레이하고
입력이 있을 경우 해당 방향으로 움직이며 달리는 애니메이션이 플레이됩니다.

```C#
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
        animator.Play("Run");
    }
    
    else
    {
        animator.Play("Idle");
    }
```

</details>

### 2. 카메라 작동
<details><summary>접기/펼치기</summary>
씬을 보여줄수 있는 카메라는 시네머신 카메라의 버추얼 카메라를 사용했습니다.
버추얼 카메라를 사용하여 플레이어를 따라오는 카메라를 쉽게 구현할수 있었으며 
시네머신 콜라이더를 사용하여 카메라가 오브젝트와 충돌할시 화면을 더욱 자연스럽게 연출했습니다.
(유니티 시네머신 카메라 인스펙터 사진 첨부)  
</details>

### 3. 플레이어 애니메이션
<details><summary>접기/펼치기</summary>
플레이어의 애니메이션은 플레이어 매니저에서 현재 상태에 따라 애니메이션이 나오도록 구현했습니다.
공격,구르기,달리기 등의 애니메이션이 플레이어의 입력값에 따라 실행될 경우
다른 애니메이션이 재생되지 못하도록 플레이어의 상태를 애니메이션 컨트롤러의 bool값으로 넣어 StateMachineBehaviour를 통해 관리했습니다.
(플레이어 애니메이터 컨트롤러 사진 첨부)

```C#
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
```

</details>

### 4. 스크립터블 오브젝트
<details><summary>접기/펼치기</summary>
게임내에서 데이터를 저장하는 용도로 스크립터블 오브젝트를 사용했습니다.
스크립터블 오브젝트는 데이터를 중복으로 생성하는 것을 방지하여 프로젝트의 메모리를 줄이는데 이점으로 발생합니다.
또한 빌드후 스크립터블 오브젝트는 데이터를 수정할 수 없고 스크립터블 오브젝트는 에셋으로 관리되기에 에셋 업데이트를 통해 수정이 가능합니다.
  
</details>

### 5. 아이템 데이터
<details><summary>접기/펼치기</summary>
아이템 데이터는 스크립터블 오브젝트를 사용하여 각 아이템의 타입과 아이템의 정보들을 저장했습니다.
(HP포션의 스크립터블 오브젝트 사진 첨부)
  
```C#
public enum ItemType
{
    WEAPON,
    ARMOR,
    POTION,
}
public class ItemData : ScriptableObject
{
    public GameObject prefab;
    public Vector3 position;
    public int id;
    public ItemType type;
    public string _name;
    public string description;
    public int value;
    public Sprite icon;
    public Sprite bigImage;
    public int rarerity;
    public int price;
    public int status;
}
```

</details>

### 6. 인벤토리
<details><summary>접기/펼치기</summary>
인벤토리는 싱글톤 패턴을 통해 인벤토리 매니저로 클래스를 관리했습니다.
인벤토리를 열때마다 인벤토리 칸의 각 아이템의 정보를 업데이트하고
아이템에 마우스 커서를 가져다댈시 아이템의 정보가 하이라이트창에서 따로 표시가 되게 했습니다.
(인벤토리와 하이라이트창 첨부)
    
<details><summary>코드 보기</summary>
        
```C#
public class InventoryManager : Singleton<InventoryManager>
{
[SerializeField] public GameObject inventory;
public Transform itemContect;
public List<ItemInventoryUI> ItemInventoryUISlots;
public delegate void OnItemChanged();
public static event OnItemChanged onItemChagedCallback;
[SerializeField] public GameObject hilightItem;
[SerializeField] Image hilightItemImage;
[SerializeField] TextMeshProUGUI hilightItemName;
[SerializeField] TextMeshProUGUI hilightItemDescription;
private void Start()
{
    ListItem();
    // 시작 할 때 아이템이 있으면 인벤토리 UI 업데이트 
}
private void Update()
{
    if (Input.GetKeyDown(KeyCode.I))
    {
        bool isActive = !inventory.activeSelf;
        inventory.SetActive(isActive); // 인벤토리 UI 활성화/비활성화 토글
                                       // 인벤토리가 활성화되면 마우스 커서를 표시하고, 그렇지 않으면 숨깁니다.
        Cursor.visible = isActive;
        // 인벤토리가 활성화되면 마우스 커서를 잠그지 않고, 그렇지 않으면 잠급니다.
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
public void Add(ItemData newItem)
{
    ItemData existingItem = PlayerInfomationManager.Instance.playerState.items.Find(item => item._name == newItem._name);
    if (existingItem != null)
    {
        existingItem.value += 1;
        // 같은 아이템이면 카운트 +1
    }
    else
    {
        newItem.value = 1;
        PlayerInfomationManager.Instance.playerState.items.Add(newItem);
        // 새로운 아이템이면 추가
    }
    onItemChagedCallback?.Invoke(); // 아이템 변경 이벤트 발생
}

public void Remove(ItemData item)
{
    ItemData itemToRemove = PlayerInfomationManager.Instance.playerState.items.Find(i => i._name == item._name);
    if (itemToRemove != null && itemToRemove.value > 0)
    {
        itemToRemove.value -= 1;
        int index = PlayerInfomationManager.Instance.playerState.items.IndexOf(itemToRemove);
        ItemInventoryUISlots[index].countItemText.text = itemToRemove.value.ToString();
        Debug.Log("포션 사용");

        if (itemToRemove.value == 0)
        {
            Debug.Log("포션 사라짐");
            ItemInventoryUISlots[index].gameObject.SetActive(false);
            PlayerInfomationManager.Instance.playerState.items.Remove(itemToRemove);
        }

        onItemChagedCallback?.Invoke();
    }
}
public void ListItem()
{
    foreach (Transform child in itemContect)
    {
        child.gameObject.SetActive(false);
        // 빈 슬롯 다 지우고
    }
    foreach (Transform child in itemContect)
    {
        if (!child.gameObject.activeSelf)
        // 빈 슬롯 상태에서
        {
            for (int i = 0; i < PlayerInfomationManager.Instance.playerState.items.Count; i++)
            {
                // 아이템 먹은 만큼 슬롯 활성화하고 UI 업데이트
                ItemInventoryUISlots[i].gameObject.SetActive(true);
                ItemInventoryUISlots[i].itemNameText.text = PlayerInfomationManager.Instance.playerState.items[i]._name;
                ItemInventoryUISlots[i].itemIconImage.sprite = PlayerInfomationManager.Instance.playerState.items[i].icon;
                ItemInventoryUISlots[i].itemBigImage.sprite = PlayerInfomationManager.Instance.playerState.items[i].bigImage;
                ItemInventoryUISlots[i].countItemText.text = $"{PlayerInfomationManager.Instance.playerState.items[i].value}";
                ItemInventoryUISlots[i].currentItemData = PlayerInfomationManager.Instance.playerState.items[i];
                // 슬롯에 커렌트 아이템을 넣어 이 아이템이 무엇인지 알게 해준다
            }
        }
    }
}
public void HilightItem(ItemData itemData)
{
    hilightItemImage.sprite = itemData.bigImage;
    hilightItemDescription.text = itemData.description;
    hilightItemName.text = itemData._name;
}
 ```
</details>
</details>

### 7. 인벤토리 슬롯
<details><summary>접기/펼치기</summary>
인벤토리 슬롯은 인벤토리 칸마다의 기능을 구현했습니다.
IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler 3개의 인터페이스를 상속받았습니다.
IPointerEnterHandler,IPointerExitHandler의 기능으로 슬롯에 커서를 가져다댈시 인벤토리 매니저에 해당 아이템의 정보를 전달함으로써 아이템 정보창이 열리게 닫히게 됩니다.
IPointerClickHandler의 경우 아이템 사용 및 장비의 장착 해제를 구현했습니다.

<details><summary>코드 보기</summary>

```C#
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager.Instance.hilightItem.transform.position = eventData.position;
        InventoryManager.Instance.hilightItem.SetActive(true);
        InventoryManager.Instance.HilightItem(currentItemData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.hilightItem.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItemData.type == ItemType.POTION)
        {
            Debug.Log("포션 마신다!");
            InventoryManager.Instance.Remove(currentItemData);
            PlayerInfomationManager.Instance.playerState.hp += 50;
            if(PlayerInfomationManager.Instance.playerState.hp >= PlayerInfomationManager.Instance.playerState.maxHp)
            {
                PlayerInfomationManager.Instance.playerState.hp = PlayerInfomationManager.Instance.playerState.maxHp;
            }
            // 포션은 소비아이템, 갯수가 0이되면 사라진다
        }
        ChangeWeapon(eventData);
        // 무기와 방어구는 계속 인벤토리에 있으면서 교체
        Time.timeScale = 1.0f;
    }

    public void ChangeWeapon(PointerEventData eventData)
    {
        if (currentItemData == null)
        {
            return;
        }
        if (currentItemData.type == ItemType.WEAPON)
        {
            // 장착 해제
            if(PlayerInfomationManager.Instance.playerState.currentWeapon == currentItemData)
            {
                PlayerInfomationManager.Instance.playerState.currentWeapon = null;
                PlayerInfomationManager.Instance.weaponEquipment.sprite = null;
                PlayerInfomationManager.Instance.playerState.attackPoint -= currentItemData.status;
            }
            // 장착중인 장비가 없을때 장비 장착
            else if (PlayerInfomationManager.Instance.playerState.currentWeapon == null)
            {
                PlayerInfomationManager.Instance.playerState.currentWeapon = currentItemData;
                PlayerInfomationManager.Instance.weaponEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.attackPoint += currentItemData.status;
            }
            // 장착중인 장비가 있을때 장비 교체
            else
            {
                PlayerInfomationManager.Instance.playerState.attackPoint -= PlayerInfomationManager.Instance.playerState.currentWeapon.status;
                PlayerInfomationManager.Instance.playerState.currentWeapon = currentItemData;
                PlayerInfomationManager.Instance.weaponEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.attackPoint += currentItemData.status;
            }
            Debug.Log("무기 장착");
        }
        else if(currentItemData.type == ItemType.ARMOR)
        {
            // 장착 해제
            if(PlayerInfomationManager.Instance.playerState.currentArmor == currentItemData)
            {
                PlayerInfomationManager.Instance.playerState.currentArmor = null;
                PlayerInfomationManager.Instance.armorEquipment.sprite = null;
                PlayerInfomationManager.Instance.playerState.defencePoint -= currentItemData.status;
            }
            // 장착중인 장비가 없을때 장비 장착
            else if(PlayerInfomationManager.Instance.playerState.currentArmor == null)
            {
                PlayerInfomationManager.Instance.playerState.currentArmor = currentItemData;
                PlayerInfomationManager.Instance.armorEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.defencePoint += currentItemData.status;
            }
            // 장착중인 장비가 있을때 장비 교체
            else
            {
                PlayerInfomationManager.Instance.playerState.defencePoint -= PlayerInfomationManager.Instance.playerState.currentArmor.status;
                PlayerInfomationManager.Instance.playerState.currentArmor = currentItemData;
                PlayerInfomationManager.Instance.armorEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.defencePoint += currentItemData.status;
            }
            Debug.Log("방어구 장착");
        }

        PlayerInfomationManager.Instance.UpdateStat();

        
        // 인벤토리에서 해당 장비를 누르면 장착
    }

```
</details>
</details>

### 8. UI 핸들러
<details><summary>접기/펼치기</summary>
UI 핸들러는 인벤토리,상점,플레이어 정보창 등 UI를 드래그 앤 드랍으로 위치를 옮길수 있는 기능입니다.
IPointerDownHandler, IDragHandler를 인터페이스로 상속받아 구현했습니다.
<details><summary>코드 보기</summary>
    
```C#
    public class InventoryHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
    
    [SerializeField]
    private Transform targetTransform; // 이동될 UI

    private Vector2 beginPoint;
    private Vector2 moveBegin;

    private void Awake()
    {
        // 이동 대상 UI를 지정하지 않은 경우, 자동으로 부모로 초기화
        if (targetTransform == null)
            targetTransform = transform.parent;
    }

    // 드래그 시작 위치 지정
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        beginPoint = targetTransform.position;
        moveBegin = eventData.position;
    }
    
    // 드래그 : 마우스 커서 위치로 이동
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        targetTransform.position = beginPoint + (eventData.position - moveBegin);
    }

 ```
</details>
</details>


### 9. 몬스터 상태 패턴
<details><summary>접기/펼치기</summary>
몬스터의 기본이 되는 스크립트를 만들면서 상태 패턴을 사용했습니다.
각각의 상태마다 조건을 달리하며 몬스터의 상태를 관리할 수 있고 유지,관리가 쉬워지는 장점이 있습니다.
이후 몬스터마다 해당 스크립트를 상속받은 뒤 각 몬스터의 정보는 스크립터블 오브젝트를 통해 가져왔습니다.
이때 스크립터블 오브젝트에 몬스터의 정보를 스크립터블 오브젝트에 바로 연결할 경우
해당 스크립터블 오브젝트를 상속받는 다른 몬스터에게도 영향이 가기 때문에
몬스터의 변수를 따로 선언하여 스크립터블 오브젝트의 데이터를 넣어줬습니다.
상속과 상태패턴,스크립터블 오브젝트를 통해 여러 종류의 몬스터를 구현하기 쉽도록 설계했습니다.


<details><summary>상태패턴 코드</summary>
    
```C#
    enum State
    {
        Idle,
        Move,
        Attack,
        Die,
    }
    public class Monster : MonoBehaviour
    {
        [SerializeField] NavMeshAgent navMeshAgent;
        [SerializeField] protected Animator animator;
        [SerializeField] protected GameObject player;
        [SerializeField] protected Collider playerWeapon;
        [SerializeField] protected BasePlayerState playerState;
        [SerializeField] protected Collider playerHitBox;
    
        State state;
    
        void Start()
        {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
    
            state = State.Idle;
            player = GameObject.FindGameObjectWithTag("Player");
            playerWeapon = GameObject.Find("LongSwordMesh").GetComponent<Collider>();
            playerHitBox = GameObject.FindGameObjectWithTag("Hit Box").GetComponent<Collider>();
        }
    
        void Update()
        {
            switch (state)
            {
                case State.Idle: Idle();
                    break;
                case State.Move: Move();
                    break;
                case State.Attack: Attack();
                    break;
                case State.Die: Die();
                    break;
            }
    
        }
    
        protected void Die()
        {
            state = State.Die;
            animator.Play("Die");
            StartCoroutine(Remove());
        }
    
    
        protected void Attack()
        {
            animator.SetTrigger("Attack");
            navMeshAgent.SetDestination(transform.position);
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            if (Vector3.Distance(transform.position, player.transform.position) >= 2)
            {
                state = State.Move;
            }
        }
    
        protected void Move()
        {
            animator.SetTrigger("Move");
            navMeshAgent.SetDestination(player.transform.position);
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            if (Vector3.Distance(transform.position, player.transform.position) < 2)
            {
                state = State.Attack;
            }
            else if(Vector3.Distance(transform.position, player.transform.position) >= 15)
            {
                state = State.Idle;
            }
        }
    
        protected void Idle()
        {
            navMeshAgent.SetDestination(transform.position);
            animator.SetTrigger("Idle");
            if (Vector3.Distance(transform.position, player.transform.position) < 15)
            {
                state = State.Move;
            }
        }
    
        IEnumerator Remove()
        {
            yield return new WaitForSeconds(10);
            gameObject.SetActive(false);
        }
    }
 ```
</details>
<details><summary>몬스터 코드</summary>

```C#
    public class Spider : Monster
{
    [SerializeField] BaseMonsterStatus monsterStatus;

    [SerializeField] int hp;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int rewardExp;
    [SerializeField] int rewardGold;
    void Awake()
    {
        hp = monsterStatus.Hp;
        attack = monsterStatus.AttackPoint;
        defence = monsterStatus.DefencePoint;
        rewardExp = monsterStatus.rewardExp;
        rewardGold = monsterStatus.rewardGold;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Animator>().GetBool("isAttacking") == true && other == playerWeapon)
        {
            animator.Play("Hit");
            hp -= playerState.attackPoint - defence;
        }
        if (player.GetComponent<Animator>().GetBool("isRolling") == false && animator.GetBool("isAttacking") == true && other == playerHitBox)
        {
            player.GetComponent<Animator>().Play("Hit");
            playerState.hp -= attack - playerState.defencePoint;
        }
    }
    private void LateUpdate()
    {
        if (hp <= 0)
        {
            Die();
        }
    }
}
```

</details>
</details>

### 10. 비동기 씬 로드
<details><summary>접기/펼치기</summary>
유니티에서는 비동기 씬 로드를 위해서 AasyncOperation 함수를 지원하고 있습니다.
AasyncOperation는 코루틴을 이용해서 비동기적 로드를 구현할 수 있게 해줍니다.
이를 이용하여 비동기 씬 로드를 구현했습니다.
<details><summary>접기/펼치기</summary>
    
```
public class SceneManagement : Singleton<SceneManagement>
{
    [SerializeField] Image screenImage;
    public void StartLoadScene(int num)
    {
        Instance.StartCoroutine(AsyncLoad(num));
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public IEnumerator FadeIn()
    {
        screenImage.gameObject.SetActive(true);
        Color color = screenImage.color;
        color.a = 1f;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            screenImage.color = color;
            if (color.a <= 0)
            {
                screenImage.gameObject.SetActive(false);
            }
        }
        yield return null;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SceneLoaded");
        StartCoroutine(FadeIn());
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    public IEnumerator AsyncLoad(int index)
    {
        screenImage.gameObject.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;
        // <asyncOperation.allowSceneActivation>
        // 장면이 준비된 즉시 장면이 활성화되는 것을 허용하는 변수입니다.
        Color color = screenImage.color;
        color.a = 0;

        // <asyncOperation.isDone>
        // 해당 동작이 완료되었는지를 나타내는 변수입니다.(읽기전용)
        while (asyncOperation.isDone == false)
        {
            color.a += Time.deltaTime;

            screenImage.color = color;

            // <asyncOperation.progress>
            // 작업의 진행 상태를 나타내는 변수입니다.(읽기전용)
            if (asyncOperation.progress >= 0.9f)
            {
                color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime);

                screenImage.color = color;
                if (color.a >= 1.0f)
                {
                    asyncOperation.allowSceneActivation = true;
                    Debug.Log("SceneLoad");
                    yield break;
                }
            }

            yield return null;
        }

    }
```

</details>

</details>

### 11. 파티클 재생
<details><summary>접기/펼치기</summary>
  
</details>

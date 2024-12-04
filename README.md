# Unity 3D RPG

## 1. 영상
[유튜브 링크](https://youtu.be/cLBc2iKuWCg)
## 2. 개발 환경 및 기간
1. Unity 2022 3.31ver
2. Visual Studio 2022
3. 개발 기간 약 6주
---
## 3. 핵심 시스템

### 1. 플레이어 이동
<details><summary>접기/펼치기</summary>
플레이어의 이동은 유니티의 Input System을 사용해서 만들었습니다.
    
먼저 GetAxisRaw를 사용하여 Horizontal과 Vertical의 값을 Vector2로 가져옵니다.

가져온 Vector2의 값의 벡터를 정규화해준 뒤 입력받은 키값의 방향으로 캐릭터가 바라보게하고
바라본 방향으로 캐릭터가 움직일수 있게 했습니다.

Input의 입력이 없을 경우 캐릭터는 제자리에 서있는 애니메이션을 플레이하고
입력이 있을 경우 해당 방향으로 움직이며 애니메이션이 플레이됩니다.

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

### 2. 시네머신 카메라
<details><summary>접기/펼치기</summary>
씬을 보여줄수 있는 카메라는 시네머신 카메라의 버추얼 카메라를 사용했습니다.
    
버추얼 카메라를 사용하여 플레이어를 따라오는 카메라를 쉽게 구현할수 있었으며
마우스의 입력값에 따라 카메라도 같이 회전되도록 했습니다.

시네머신 콜라이더를 사용하여 카메라가 오브젝트와 충돌할시 화면을 더욱 자연스럽게 연출했습니다.
    
![시네머신카메라](https://github.com/user-attachments/assets/3d3c5904-ac67-4918-82aa-3ec96ef6d16f)
</details>

### 3. 캐릭터 애니메이션
<details><summary>접기/펼치기</summary>
플레이어의 애니메이션은 플레이어 매니저에서 현재 상태에 따라 애니메이션이 나오도록 구현했습니다.
    
공격,구르기,달리기 등의 애니메이션이 플레이어가 입력한 값에 따라 실행될 경우

다른 애니메이션이 재생되지 못하도록 플레이어의 상태를 애니메이션 컨트롤러의 bool값으로 넣어 StateMachineBehaviour를 통해 관리했습니다.
    
![애니메이터](https://github.com/user-attachments/assets/1d2940a1-1ea1-476c-ada6-95e59f5c917f)

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

### 4. 플레이어 데이터
<details><summary>접기/펼치기</summary>
게임내에서 데이터를 저장하는 용도로 스크립터블 오브젝트를 사용했습니다.
    
스크립터블 오브젝트는 데이터를 중복으로 생성하는 것을 방지하여 프로젝트의 메모리를 줄이는데 이점으로 발생합니다.

또한 빌드후 스크립터블 오브젝트는 데이터를 수정할 수 없고 스크립터블 오브젝트는 에셋으로 관리되기에 에셋 업데이트를 통해 수정이 가능합니다.

![스크립터블오브젝트](https://github.com/user-attachments/assets/91704279-9080-488a-91a0-248947cb2a59)

</details>

### 5. 아이템 데이터
<details><summary>접기/펼치기</summary>
아이템 데이터는 스크립터블 오브젝트를 사용하여 각 아이템의 타입과 아이템의 정보들을 저장했습니다.
    
![아이템데이터](https://github.com/user-attachments/assets/c71f64c9-57af-4b11-9b90-2b0369922bb0)
  
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

![인벤토리와 하이라이트창](https://github.com/user-attachments/assets/c6fb883f-0a99-4529-b780-5abffad99df0)

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

IPointerEnterHandler,IPointerExitHandler의 기능으로 슬롯에 커서를 가져다댈시 인벤토리 매니저에 해당 아이템의 정보를 전달함으로써 아이템 정보창이 열리고 닫히게 됩니다.

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

### 8. 상점
<details><summary>접기/펼치기</summary>
상점은 스크롤바와 버티컬 레이아웃을 사용하여 아이템을 정렬한뒤,
각각의 아이템의 정보를 넣고 구매 버튼으로 아이템에 해당하는 가격을 지불하여
인벤토리에 아이템이 추가되도록 구현했습니다.

![상점](https://github.com/user-attachments/assets/8b846a35-e438-4ea5-8bfb-d970a4d7950f)

<details><summary>코드 보기</summary>

```C#
List<ItemData> shopItems = new List<ItemData>();
public List<ShopSlots> shopSlots;
[SerializeField] ItemData armor;
[SerializeField] ItemData armor2;
[SerializeField] ItemData weapon;
[SerializeField] ItemData weapon2;
[SerializeField] ItemData potion;
public Transform itemContect;


private void Start()
{
    shopItems.Add(armor);
    shopItems.Add(armor2);
    shopItems.Add(weapon);
    shopItems.Add(weapon2);
    shopItems.Add(potion);
    
    ListItem();
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
            //빈 슬롯 상태에서
        {
            for (int i = 0; i < shopItems.Count; i++)
            {
                // 아이템 먹은 만큼 슬롯 활성화하고 UI 업데이트
                shopSlots[i].gameObject.SetActive(true);
                shopSlots[i].itemNameText.text = shopItems[i]._name;
                shopSlots[i].itemIconImage.sprite = shopItems[i].icon;
                shopSlots[i].itemBigImage.sprite = shopItems[i].bigImage;
                shopSlots[i].itemPrice.text = $"{shopItems[i].price}";
                shopSlots[i].itemDescription.text = shopItems[i].description;
                shopSlots[i].currentItemData = shopItems[i];
                // 슬롯에 커렌트 아이템을 넣어 이 아이템이 무엇인지 알게 해준다
            }
        }
    }
}
public void BuyItem()
{
    if(PlayerInfomationManager.Instance.playerState.gold >= currentItemData.price)
    {
        PlayerInfomationManager.Instance.playerState.gold -= currentItemData.price;
        InventoryManager.Instance.Add(currentItemData);
        InventoryManager.Instance.ListItem();
    }
}
```

</details>
</details>

### 9. UI 핸들러
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


### 10. 몬스터 상태 패턴
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
    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    protected bool isInteracting;
    protected bool isDie;
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
        isInteracting = animator.GetBool("isInteracting");
        switch (state)
        {
            case State.Idle: Idle();
                break;
            case State.Move: Move();
                break;
            case State.Attack: Attack();
                break;
        }

    }

    protected void Die()
    {
        state = State.Die;
        animator.Play("Die");
        StartCoroutine(Remove());
        isDie = true;
    }


    protected void Attack()
    {
        if(isInteracting == false)
        {
            animator.Play("Attack");
        }
        navMeshAgent.SetDestination(transform.position);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if (Vector3.Distance(transform.position, player.transform.position) >= 1.5f && isInteracting == false)
        {
            state = State.Move;
        }
    }

    protected void Move()
    {
        animator.Play("Move");
        navMeshAgent.SetDestination(player.transform.position);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
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
        animator.Play("Idle");
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

    private Collider capsuleCollider;
    void Awake()
    {
        hp = monsterStatus.Hp;
        attack = monsterStatus.AttackPoint;
        defence = monsterStatus.DefencePoint;
        rewardExp = monsterStatus.rewardExp;
        rewardGold = monsterStatus.rewardGold;
        capsuleCollider = GetComponent<Collider>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (isDie == true)
        {
            return;
        }
        if (player.GetComponent<Animator>().GetBool("isRolling") == false && animator.GetBool("isAttacking") == true && other == playerHitBox && animator.GetBool("AttackCount") == false)
        {
            player.GetComponent<Animator>().Play("Hit");
            animator.SetBool("AttackCount", true);

            if ((attack - playerState.defencePoint) < 0)
            {
                return;
            }
            else
            {
                playerState.hp -= attack - playerState.defencePoint;
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Animator>().GetBool("isAttacking") == true && other == playerWeapon)
        {
            animator.Play("Hit");

            if ((playerState.attackPoint - defence) < 0)
            {
                return;
            }
            else
            {
                hp -= playerState.attackPoint - defence;
                if (hp <= 0)
                {
                    Die();
                    Reward();
                    SoundManager.Instance.PlayEffect("WolfDie");
                    capsuleCollider.enabled = false;
                }
            }
        }
    }

    void Reward()
    {
        playerState.currentExp += rewardExp;
        playerState.gold += rewardGold;
    }
    void Attack()
    {
        base.Attack();
        SoundManager.Instance.PlayEffect("SpiderAttack");
    }
}

```

</details>
</details>

### 11. 비동기 씬 로드
<details><summary>접기/펼치기</summary>
유니티에서는 비동기 씬 로드를 위해서 AasyncOperation 함수를 지원하고 있습니다.
    
AasyncOperation는 코루틴을 이용해서 비동기적 로드를 구현할 수 있게 해줍니다.

이를 이용하여 비동기 씬 로드를 구현했습니다.
<details><summary>접기/펼치기</summary>
    
```C#
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

### 12. 파티클 재생
<details><summary>접기/펼치기</summary>
파티클은 플레이어 캐릭터의 애니메이션 타이밍에 맞춰서 재생되도록 만들었습니다.
    
공격을 휘두르는 애니메이션에 이벤트를 등록하여 해당 파티클의 함수명과 List 인덱스를 호출하여
애니메이션이 동작중에 파티클이 같이 플레이 되도록 만들었습니다.

![애니메이션이벤트파티클](https://github.com/user-attachments/assets/6064e6bf-9365-414d-a401-e44ef6c0a150)

```C#
public class ParticleManager : Singleton<ParticleManager>
{
    [SerializeField] public ParticleSystem[] particleSystems;

    void ParticlePlay(int num)
    {
        particleSystems[num].Play();
    }
}
```

</details>

### 13. 사운드 매니저
<details><summary>접기/펼치기</summary>
사운드 매니저는 싱글톤으로 클래스를 관리하였으며 리소스 폴더에 있는 사운드클립을 딕셔너리에 저장하여 전역에서 오디오클립을 플레이할 수 있도록 했습니다.

```C#
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource footstepSource;
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private void Start()
    {
        LoadAudioClips();
    }
    public void PlayMusic(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            musicSource.clip = audioClips[clipName];
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    public void PlayEffect(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            effectsSource.PlayOneShot(audioClips[clipName]);
        }
    }
    private void LoadAudioClips()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds");
        foreach (AudioClip clip in clips)
        {
            if (!audioClips.ContainsKey(clip.name))
            {
                audioClips.Add(clip.name, clip);
            }
        }
    }
    public void PlayFootstep(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            footstepSource.clip = audioClips[clipName];
            footstepSource.loop = true;
            if (!footstepSource.isPlaying) footstepSource.Play();
        }
    }

    public void StopFootstep()
    {
        if (footstepSource.isPlaying)
        {
            footstepSource.Stop();
        }
    }
```
</details>

## 4. 최적화 기능
### 1. 오클루전 컬링
<details><summary>접기/펼치기</summary>
오클루전 컬링은 다른 오브젝트에 가려진 카메라에 보이지않는 오브젝트를 렌더링하지 않음으로써 렌더링속도를 향상 시켜주는 최적화 기법입니다.
    
오브젝트는 오클루더와 오클루디로 나누어지며 오클루더는 가려진 오브젝트와 오브젝트를 가리는 오브젝트 두개를 포함하며
오클루디는 다른 오브젝트를 가리는 오브젝트만을 의미합니다.

![오클루전컬링](https://github.com/user-attachments/assets/c6ea8a3c-c47f-44c9-b04f-6ecaf4854850)
    
</details>

### 2. LOD(Level of Detail)
<details><summary>접기/펼치기</summary>
LOD는 오브젝트를 카메라에서 렌더링하는 거리에 따라 여러 단계로 나눠서 렌더링하는 기술입니다.
    
오브젝트가 카메라와 거리가 멀 경우 디테일한 렌더링이 요구되지 않기 때문에 낮은 텍스쳐로 렌더링을 하면서
렌더링 속도를 향상시키는 기법입니다.

![LOD](https://github.com/user-attachments/assets/44e36b5b-4a1b-46ac-865a-a53a3649d95c)

</details>

### 3. GPU Instancing
<details><summary>접기/펼치기</summary>

GPU 인스턴싱은 유니티에서 드로아 콜을 줄이기 위해 사용하는 최적화 기법 중 하나입니다.

씬내에서 같은 메시를 동시에 렌더링 하여 렌더링 속도를 향상시켜줍니다.

메테리얼 인스펙터 창 하단의 어드밴스 옵션에서 GPU 인스턴싱을 사용할 수 있습니다.

단, RenderPipe에서 SRP Batcher와 동시에 사용이 불가능하니 둘 중 하나만 사용할 수 있습니다.

![GPU인스턴싱](https://github.com/user-attachments/assets/021a1cac-4b49-426b-b232-2423e7be8c2a)

</details>

## 5. 추가 구현 사항

### 서버에 데이터를 저장,로드기능
<details><summary>접기/펼치기</summary>
Unity의 ScriptableObject를 사용하여 게임 내 플레이어의 상태 데이터를 관리하고, PlayFab을 이용하여 클라우드에 해당 데이터를 저장하고 로드하는 기능을 구현했습니다. ScriptableObject는 Unity에서 데이터를 관리하는 용도로 매우 유용하며, 이를 JSON 형식으로 직렬화(Serialization)하여 PlayFab 서버에 저장하는 방식으로, 게임의 클라우드 기반 데이터 관리를 처리합니다.

#### 1. ScriptableObject 데이터를 JSON으로 직렬화하여 PlayFab에 저장
플레이어의 상태 데이터는 ToJson() 메서드를 통해 JSON 형식으로 변환되어 PlayFab에 저장됩니다. JSON 직렬화 과정을 통해 데이터를 텍스트 형식으로 변환하여, 네트워크를 통해 서버와 클라이언트 간에 쉽게 전송할 수 있습니다.

~~~c#
public string ToJson()
{
    var data = new PlayerStateData
    {
        level = level,
        hp = hp,
        maxHp = maxHp,
        stamina = stamina,
        maxStamina = maxStamina,
        attackPoint = attackPoint,
        defencePoint = defencePoint,
        currentWeaponId = currentWeapon.id,
        currentArmorId = currentArmor.id,
        currentExp = currentExp,
        maxExp = maxExp,
        gold = gold,
        items = items.ConvertAll(item => new PlayerStateData.ItemInventoryData
        {
            id = item.id,
            value = item.value
        })
    };

    return JsonUtility.ToJson(data, true);
}

~~~

#### 2. PlayFab 서버에 데이터 저장
플레이어 상태 데이터는 PlayFab 클라이언트 API를 사용하여 서버에 저장됩니다. UpdateUserDataRequest를 사용하여 PlayerState라는 키로 데이터를 저장합니다.

~~~c#
public void SavePlayerStateToPlayFab()
{
    string json = playerState.ToJson(); // ScriptableObject 데이터를 JSON으로 직렬화
    var request = new UpdateUserDataRequest
    {
        Data = new Dictionary<string, string> { { "PlayerState", json } }
    };

    PlayFabClientAPI.UpdateUserData(request,
        result => Debug.Log("플레이어 데이터가 PlayFab에 저장되었습니다."),
        error => Debug.LogError("데이터 저장 실패: " + error.GenerateErrorReport()));
}

~~~

#### 3. PlayFab 서버에서 데이터 로드
저장된 데이터를 로드할 때는 GetUserDataRequest를 사용하여 PlayFab 서버에서 플레이어 데이터를 조회합니다.
JSON 데이터를 불러온 후, LoadFromJson() 메서드를 통해 이를 BasePlayerState 객체로 다시 변환하고, 로드된 아이템과 장착 아이템을 ScriptableObject로 인스턴스화하여 게임 내에 반영합니다.

~~~c#
public void LoadPlayerStateFromPlayFab()
{
    PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
        result =>
        {
            if (result.Data != null && result.Data.ContainsKey("PlayerState"))
            {
                string json = result.Data["PlayerState"].Value;
                playerState.LoadFromJson(json); // JSON 데이터를 로드하여 ScriptableObject에 적용
            }
        },
        error => Debug.LogError("플레이어 상태 로드 실패: " + error.GenerateErrorReport()));
}
~~~

#### 4. 아이템 및 장착 아이템 처리
아이템과 장착 아이템(currentWeapon, currentArmor)은 서버에서 로드한 JSON 데이터에서 아이템 ID를 기반으로 Resources.LoadAll<ItemData>("Items")를 사용하여 아이템을 로드하고, 인스턴스화하여 장착 아이템과 인벤토리에 반영합니다.

~~~c#
ItemData[] itemDatabase = Resources.LoadAll<ItemData>("Items");
currentWeapon = Array.Find(itemDatabase, item => item.id == data.currentWeaponId);
currentArmor = Array.Find(itemDatabase, item => item.id == data.currentArmorId);

items = new List<ItemData>();
foreach (var itemData in data.items)
{
    var item = Array.Find(itemDatabase, i => i.id == itemData.id);
    if (item != null)
    {
        var itemInstance = Instantiate(item);
        itemInstance.value = itemData.value;
        items.Add(itemInstance);
    }
}
~~~

### 문제 발생 및 해결 과정

#### **1. 초기 프로젝트 설정과 문제 발생**
프로젝트 초기에는 플레이어의 모든 데이터를 스크립터블 오브젝트(ScriptableObject)에 저장하는 방식으로 개발을 진행했습니다. 이때는 서버에 데이터를 저장할 필요가 없다고 판단하여 서버 연동을 고려하지 않은 설계가 이루어졌습니다.
하지만 강사님과 멘토님의 추천으로 후속 작업에서 **PlayFab 서버와 데이터 연동**을 시도하던 중, 서버에 데이터를 저장하는 부분에서 오류가 발생했습니다. 구체적으로, 서버에 데이터를 저장하려고 할 때, 직렬화되지 않은 객체들 (예: `ItemData`클래스 또는 `Sprite` 등) 때문에 문제가 발생했습니다.

#### **2. 스크립터블 오브젝트 직렬화 문제**
서버에 데이터를 저장하기 위해서는 **스크립터블 오브젝트 데이터를 JSON 형식으로 직렬화**해야 했습니다. 그러나, `ItemData`와 `Sprite`와 같은 Unity에서만 사용하는 객체는 JSON 직렬화가 불가능했습니다.
그 결과, **직렬화 시 에러**가 발생했고, 데이터를 PlayFab 서버에 저장할 수 없었습니다.

#### **3. 해결책: 필요한 데이터만 직렬화**
이 문제를 해결하기 위해, `ItemData`와 `Sprite` 같은 객체는 **직접 직렬화할 수 없으므로** 이를 대신할 수 있는 **고유 ID와 관련 값(value)만을 저장**하는 방식으로 수정했습니다.
예를 들어, `ItemData` 객체에서 아이템의 고유 ID와 아이템의 수량(value)만을 저장하여, 서버와의 데이터 동기화가 가능하도록 했습니다. 이후, 서버에서 로드할 때, 아이템의 ID를 통해 로컬 리소스에서 해당 아이템을 다시 불러오는 방식으로 처리했습니다.

~~~c#
    // 아이템의 ID와 수량만 저장하는 방법
    [System.Serializable]
    public class ItemInventoryData
    {
        public int id;    // 아이템 ID
        public int value; // 아이템 수량
    }
~~~
#### **4. 리소스를 미리 로드하는 방법**
또한, 데이터를 미리 로드하는 방법을 사용하여 한 번에 많은 데이터를 저장할 필요가 없다는 것을 알게 되었습니다.
아이템과 같은 데이터를 고유 ID와 수량으로만 저장하면, 서버에서 데이터를 로드할 때 리소스에서 미리 로드된 아이템을 재사용할 수 있습니다. 이렇게 함으로써 아이템 정보를 한 번만 로드하고, 고유 ID를 기준으로 필요한 아이템만을 불러오는 방식으로 최적화할 수 있었습니다.
~~~c#
    // 고유 ID를 기준으로 아이템을 리소스에서 로드
    ItemData[] itemDatabase = Resources.LoadAll<ItemData>("Items");
    ItemData item = Array.Find(itemDatabase, i => i.id == itemData.id);
~~~
#### **5. Json과 ScriptableObject에 대한 이해 증진**
이 과정에서 **JSON 직렬화와 ScriptableObject의 동작 원리**에 대해 더 깊이 공부할 수 있었습니다. JSON 형식은 데이터를 네트워크를 통해 효율적으로 전달하는 데 매우 유용하며, **스크립터블 오브젝트는 Unity에서 데이터를 관리하고 저장하는 데 유용한 툴**임을 확인했습니다. 하지만, **ScriptableObject를 그대로 JSON으로 직렬화할 수 없다는 제약**이 있다는 점에서 **직렬화 가능한 데이터 구조로 변환하는 과정**에 대한 이해가 필요하다는 것을 배웠습니다.

#### **6. PlayFab을 이용한 데이터 동기화**
PlayFab을 사용하여 **서버와 클라이언트 간의 데이터 동기화** 및 **플랫폼 간 데이터 공유**를 진행하는 기초적인 작업을 진행할 수 있었습니다. 이를 통해 **플레이어의 데이터**가 **서버에 저장되고, 다른 장치에서 로드**할 수 있다는 점에서 **클라우드 데이터 관리의 중요성**을 실감할 수 있었습니다.

#### **7. 결론**
이 프로젝트를 통해 **게임 데이터의 클라우드 저장 및 로드** 기능을 구현하는 과정에서 다양한 문제를 해결하였고, **스크립터블 오브젝트와 JSON 직렬화**에 대해 더 깊이 이해할 수 있었습니다. **PlayFab 서버와의 데이터 동기화**는 기초적인 부분이었지만, **데이터 관리와 플랫폼 간 데이터 공유**에 대한 중요한 경험을 얻을 수 있는 시간이었습니다.

</details>

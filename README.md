#Unity 3D RPG
---
##1. 개발 기간
약 1.5개월

##2. 개발 환경
1. Unity 2022
2. Visual Studio 2022
   
##3. 핵심 시스템

### 플레이어 이동
<details><summary>접기/펼치기</summary>
플레이어의 이동은 유니티의 Input System을 사용해서 만들었습니다.
먼저 GetAxisRaw를 사용하여 Horizontal과 Vertical의 값을 Vector2로 가져옵니다.
가져온 Vector2의 값의 벡터를 정규화해준 뒤 입력받은 키값의 방향으로 캐릭터가 바라보게하고
바라본 방향으로 캐릭터가 움직일수 있게 했습니다.

Input의 입력이 없을 경우 캐릭터는 제자리에 서있는 애니메이션을 플레이하고
입력이 있을 경우 해당 방향으로 움직이며 달리는 애니메이션이 플레이됩니다.
```
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

### 카메라 작동
<details><summary>접기/펼치기</summary>
씬을 보여줄수 있는 카메라는 시네머신 카메라의 버추얼 카메라를 사용했습니다.
버추얼 카메라를 사용하여 플레이어를 따라오는 카메라를 쉽게 구현할수 있었으며 
시네머신 콜라이더를 사용하여 카메라가 오브젝트와 충돌할시 화면을 더욱 자연스럽게 연출했습니다.
(유니티 시네머신 카메라 인스펙터 사진 첨부)  
</details>

### 플레이어 애니메이션
<details><summary>접기/펼치기</summary>
플레이어의 애니메이션은 플레이어 매니저에서 현재 상태에 따라 애니메이션이 나오도록 구현했습니다.
공격,구르기,달리기 등의 애니메이션이 플레이어의 입력값에 따라 실행될 경우
다른 애니메이션이 재생되지 못하도록 플레이어의 상태를 애니메이션 컨트롤러의 bool값으로 넣어 StateMachineBehaviour를 통해 관리했습니다.
(플레이어 애니메이터 컨트롤러 사진 첨부)

```
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

### 스크립터블 오브젝트
<details><summary>접기/펼치기</summary>
게임내에서 데이터를 저장하는 용도로 스크립터블 오브젝트를 사용했습니다.
스크립터블 오브젝트는 데이터를 중복으로 생성하는 것을 방지하여 프로젝트의 메모리를 줄이는데 이점으로 발생합니다.
또한 빌드후 스크립터블 오브젝트는 데이터를 수정할 수 없고 스크립터블 오브젝트는 에셋으로 관리되기에 에셋 업데이트를 통해 수정이 가능합니다.
  
<details>

### 아이템 데이터
<details><summary>접기/펼치기</summary>
아이템 데이터는 스크립터블 오브젝트를 사용하여 각 아이템의 타입과 아이템의 정보들을 저장했습니다.
(HP포션의 스크립터블 오브젝트 사진 첨부)
  
```
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
<details>

### 인벤토리
<details><summary>접기/펼치기</summary>

<details>

### UI 핸들러
<details><summary>접기/펼치기</summary>

<details>


### 몬스터 상태 패턴
<details><summary>접기/펼치기</summary>

<details>

### 비동기 씬 로드
<details><summary>접기/펼치기</summary>

<details>

### 파티클 재생
<details><summary>접기/펼치기</summary>
  
<details>

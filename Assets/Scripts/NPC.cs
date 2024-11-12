using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject interactionNPC;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI _name;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if(Vector3.Distance(player.transform.position,gameObject.transform.position) <= 2 && Input.GetKeyDown(KeyCode.F))
        {
            bool isActive = !interactionNPC.activeSelf;

            interactionNPC.SetActive(isActive); // UI 활성화/비활성화 토글
                                                // UI가 활성화되면 마우스 커서를 표시하고, 그렇지 않으면 숨깁니다.
            Cursor.visible = isActive;
            // UI가 활성화되면 마우스 커서를 잠그지 않고, 그렇지 않으면 잠급니다.
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
        _name.transform.rotation = Camera.main.transform.rotation;
        
    }
}

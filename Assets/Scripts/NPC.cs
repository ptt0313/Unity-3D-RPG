using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject interactionNPC;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI _name;

    bool isActive;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerNPC()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            interactionNPC.SetActive(isActive);
        }
    }
    private void Update()
    {
        if(Vector3.Distance(player.transform.position,gameObject.transform.position) <= 2)
        {
            OnTriggerNPC();
        }
        _name.transform.rotation = Camera.main.transform.rotation;
        isActive = !interactionNPC.activeSelf;
    }
}

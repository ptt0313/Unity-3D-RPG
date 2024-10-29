using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject interactionNPC;
    [SerializeField] TextMeshProUGUI _name;
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            interactionNPC.SetActive(true);
        }
    }
    private void Update()
    {
        _name.transform.rotation = Camera.main.transform.rotation;
    }
}

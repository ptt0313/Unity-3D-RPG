using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] BasePlayerState playerState;
    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void Start()
    {
        slider.maxValue = playerState.maxStamina;
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerState.stamina;
    }
}

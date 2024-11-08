using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
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
        slider.maxValue = playerState.maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerState.hp;
    }
}

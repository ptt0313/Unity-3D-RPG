using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] public int gold = 500;
    void Start()
    {         
        goldText.text = gold.ToString();
    }

    void Update()
    {
        UpdateGold();
    }

    void UpdateGold()
    {
        goldText.text = gold.ToString();
    }
}

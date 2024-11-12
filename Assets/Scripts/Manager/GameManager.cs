using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] BasePlayerState state;

    void Update()
    {
        LevelUp();
    }
    void LevelUp()
    {
        if (state.currentExp >= state.maxExp)
        {
            state.currentExp -= state.maxExp;
            state.level += 1;
            state.attackPoint += 3;
            state.defencePoint += 3;
            state.maxHp += 5;
            state.maxStamina += 5;
            state.maxExp = state.level * 100;
            ParticleManager.Instance.ParticlePlay(2);
        }
    }
}

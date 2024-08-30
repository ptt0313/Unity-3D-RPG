using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    enum State
    {
        Idle,
        Move,
        Attack
    }
    State state;

    void Start()
    {
        state = State.Idle;
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;

            case State.Move:
                break;

            case State.Attack:
                break;
        }

    }
}

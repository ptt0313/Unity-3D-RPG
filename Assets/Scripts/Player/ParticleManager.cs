using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    [SerializeField] public ParticleSystem[] particleSystems;

    void ParticlePlay(int num)
    {
        particleSystems[num].Play();
    }
}

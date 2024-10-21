using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yonguk;

public class PlayerAttacker : MonoBehaviour
{
    AnimatorHandler animatorHandler;
    [SerializeField] ParticleSystem particle;
    [SerializeField] ParticleSystem particle2;

    void Start()
    {
        animatorHandler = GetComponent<AnimatorHandler>();
    }
    public void RightAttack()
    {
        animatorHandler.PlayTargetAnimation("Attack", true);
        particle.Play();
    }
    public void LeftAttack()
    {
        animatorHandler.PlayTargetAnimation("Attack2", true);
        particle2.Play();
    }
}

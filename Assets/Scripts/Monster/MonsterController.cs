using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    Transform transform;
    Transform playerTransform;
    NavMeshAgent nvAgent;
    void Start()
    {
        transform = this.gameObject.GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if(Vector3.Distance(transform.position,playerTransform.position) < 10.0f)
        {
            nvAgent.destination = playerTransform.position;
        }
    }
}

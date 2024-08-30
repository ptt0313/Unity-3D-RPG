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
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nvAgent.destination = playerTransform.position;
    }
}

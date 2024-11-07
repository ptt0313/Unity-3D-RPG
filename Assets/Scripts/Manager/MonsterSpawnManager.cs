using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [SerializeField] List<Transform> monsterSpawnPoint;
    [SerializeField] GameObject monsterPrefab;
    int random;
    
    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(15f);

        random = Random.Range(0,monsterSpawnPoint.Count);

        Instantiate(monsterPrefab,monsterSpawnPoint[random]);
    }
    private void Start()
    {
        StartCoroutine(SpawnMonster());
    }
}

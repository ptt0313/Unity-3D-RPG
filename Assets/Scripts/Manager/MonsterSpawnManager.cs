using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [SerializeField] List<Transform> monsterSpawnPoint;
    [SerializeField] GameObject[] monster;
    
    int random;
    int randomMonster;
    IEnumerator SpawnMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);

            random = Random.Range(0,monsterSpawnPoint.Count);
            randomMonster = Random.Range(0, monster.Length);
            Instantiate(monster[randomMonster],monsterSpawnPoint[random]);

        }
    }
    private void Start()
    {
        StartCoroutine(SpawnMonster());
    }
}

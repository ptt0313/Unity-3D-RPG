using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSpawnManager : Singleton<PlayerSpawnManager>
{
    [SerializeField] GameObject playerCharacter;
    Transform spawnPosition;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        spawnPosition = GameObject.FindGameObjectWithTag("Spawn Position").transform;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void Start()
    {
        Instantiate(playerCharacter,spawnPosition.position,Quaternion.identity);
    }
}

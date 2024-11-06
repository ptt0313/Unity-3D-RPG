using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSpawnManager : Singleton<PlayerSpawnManager>
{
    Transform spawnPosition;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        spawnPosition = GameObject.FindGameObjectWithTag("Spawn Position").transform;
        Instantiate(Resources.Load("unitychan"), spawnPosition);
        Instantiate(Resources.Load("Virtual Camera"));
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}

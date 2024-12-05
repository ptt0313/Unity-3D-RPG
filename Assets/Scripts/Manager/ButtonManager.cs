using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void Title()
    {
        SceneManagement.Instance.StartLoadScene(0);
        SoundManager.Instance.PlayMusic("Title");
        Time.timeScale = 1;
    }
    public void Town()
    {
        SceneManagement.Instance.StartLoadScene(1);
        SoundManager.Instance.PlayMusic("Town");
    }
    public void Forest()
    {
        SceneManagement.Instance.StartLoadScene(2);
        SoundManager.Instance.PlayMusic("Forest");
    }
    public void Dungeon()
    {
        SceneManagement.Instance.StartLoadScene(3);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

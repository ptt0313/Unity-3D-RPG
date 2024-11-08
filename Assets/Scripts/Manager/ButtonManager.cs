using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    
    public void Town()
    {
        SceneManagement.Instance.StartLoadScene(0);
    }
    public void Forest()
    {
        SceneManagement.Instance.StartLoadScene(1);
    }
    public void Dungeon()
    {
        SceneManagement.Instance.StartLoadScene(2);
    }
}

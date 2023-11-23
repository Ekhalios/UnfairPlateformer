using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int startScene;

    public void onClickLevel1()
    {
        SceneManager.LoadScene(2);
    }
    public void onClickLevel2()
    {
        SceneManager.LoadScene(3);
    }
    public void onClickLevel3()
    {
        SceneManager.LoadScene(4);
    }
    public void onClickLevel4()
    {
        SceneManager.LoadScene(5);
    }
    public void onClickLevel5()
    {
        SceneManager.LoadScene(6);
    }

    public void onClickMenu()
    {
        SceneManager.LoadScene(0);
    }
}

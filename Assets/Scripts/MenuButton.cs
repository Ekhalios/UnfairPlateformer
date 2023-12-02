using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public int startScene;

    public void onClickStart()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void onClickEditor()
    {
        SceneManager.LoadScene("mapSelector");
    }
    public void onClickMulti()
    {
        SceneManager.LoadScene("Loading");
    }

    public void onClickQuit()
    {
        Application.Quit();
    }
}

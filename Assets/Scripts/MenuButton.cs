using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public int startScene;

    public void onClickStart()
    {
        SceneManager.LoadScene(startScene);
    }

    public void onClickQuit()
    {
        Application.Quit();
    }
}

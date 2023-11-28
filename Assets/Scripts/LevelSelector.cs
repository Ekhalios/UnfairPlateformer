using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int startScene;

    public void onClickLevel1()
    {
        PlayerPrefs.SetString("MapFileName", "Level1");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void onClickLevel2()
    {
        PlayerPrefs.SetString("MapFileName", "Level2");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void onClickLevel3()
    {
        PlayerPrefs.SetString("MapFileName", "Level3");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void onClickLevel4()
    {
        PlayerPrefs.SetString("MapFileName", "Level4");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void onClickLevel5()
    {
        PlayerPrefs.SetString("MapFileName", "Level5");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }

    public void onClickMenu()
    {
        SceneManager.LoadScene(0);
    }
}

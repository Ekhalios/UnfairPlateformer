using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int startScene;

    public void onClickLevel1()
    {
        PlayerPrefs.SetString("MapFileName", "/Maps/Level1");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void onClickLevel2()
    {
        PlayerPrefs.SetString("MapFileName", "/Maps/Level2");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void onClickLevel3()
    {
        PlayerPrefs.SetString("MapFileName", "/Maps/Level3");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void onClickLevel4()
    {
        PlayerPrefs.SetString("MapFileName", "/Maps/Level4");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void onClickLevel5()
    {
        PlayerPrefs.SetString("MapFileName", "/Maps/Level5");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }

    public void onClickDidactitiel()
    {
        SceneManager.LoadScene("Didactitiel");
    }

    public void onClickMenu()
    {
        SceneManager.LoadScene(0);
    }
}

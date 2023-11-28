using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public GameObject panelMenu;
    public PlayerMovement movement;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelMenu.activeSelf)
            {
                panelMenu.SetActive(false);
            } else
            {
                panelMenu.SetActive(true);
            }
            movement.switchControlable();
        }
    }

    public void backMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void backGame()
    {
        panelMenu.SetActive(false);
        movement.switchControlable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    private int bestDeathScore = 0;
    private float bestTimer = 0;
    private bool finished = false;
    private string timerText;
    private string deathText;
    public TextMeshProUGUI highScoreText;
    private MapCreator mapCreator;

    void Start()
    {
        mapCreator = FindObjectOfType<MapCreator>();
        bestDeathScore = PlayerPrefs.GetInt(mapCreator.mapName + "Death", 0);
        bestTimer = PlayerPrefs.GetFloat(mapCreator.mapName + "Timer", 0.0f);
        Debug.Log(bestDeathScore + ", " + bestTimer);
    }

    public void newScore(int death, float timer)
    {
        if (!finished)
        {
            bestDeathScore = death;
            deathText = "Death: " + bestDeathScore.ToString();
            bestTimer = timer;
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            float milliseconds = (timer % 1) * 1000; // Obtenir les millisecondes

            timerText = string.Format("Timer: {0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
            highScoreText.text = deathText + " | " + timerText;
            PlayerPrefs.SetInt(mapCreator.mapName + "Death", bestDeathScore);
            PlayerPrefs.SetFloat(mapCreator.mapName + "Timer", bestTimer);
            finished = true;
        } else
        {
            if (bestDeathScore > death)
            {
                bestDeathScore = death;
                deathText = "Death: " + bestDeathScore.ToString();
                highScoreText.text = deathText + " | " + timerText;
                PlayerPrefs.SetInt(mapCreator.mapName + "Death", bestDeathScore);
            }
            if (bestTimer > timer)
            {
                bestTimer = timer;
                float minutes = Mathf.FloorToInt(timer / 60);
                float seconds = Mathf.FloorToInt(timer % 60);
                float milliseconds = (timer % 1) * 1000; // Obtenir les millisecondes

                // Mettre à jour le texte du chronomètre
                timerText = string.Format("Timer: {0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
                highScoreText.text = deathText + " | " + timerText;
                PlayerPrefs.SetFloat(mapCreator.mapName + "Timer", bestTimer);
            }
        }
        PlayerPrefs.Save();
    }
}

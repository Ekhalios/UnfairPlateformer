using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chronoScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 0f;
    private bool chrono = true;

    void Start()
    {
        timerText.color = Color.black;
    }
    void Update()
    {
        if (!chrono)
        {
            return;
        }
        timer += Time.deltaTime;

        // Convertir le temps en minutes et secondes
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        float milliseconds = (timer % 1) * 1000; // Obtenir les millisecondes

        // Mettre à jour le texte du chronomètre
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void resetChrono()
    {
        timer = 0f;
    }

    public void setChrono(bool active)
    {
        chrono = active;
    }

    public float getChrono()
    {
        return timer;
    }
}

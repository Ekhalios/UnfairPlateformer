using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chronoScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 0f;

    void Start()
    {
        timerText.color = Color.black;
    }
    void Update()
    {
        // Augmenter le temps du chronom�tre
        timer += Time.deltaTime;

        // Convertir le temps en minutes et secondes
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        float milliseconds = (timer % 1) * 1000; // Obtenir les millisecondes

        // Mettre � jour le texte du chronom�tre
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void resetChrono()
    {
        timer = 0f;
    }
}

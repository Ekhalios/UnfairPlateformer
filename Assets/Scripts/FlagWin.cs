using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagWin : MonoBehaviour
{
    public AudioSource winSoundEffect;
    private PlayerMovement playerMovement;
    private PlayerDeath playerDeath;
    private GameObject menuWin;
    private bool played = false;
    private MapCreator creator;
    private chronoScript chrono;
    private PlayerScore score;

    private void Start()
    {
        creator = FindObjectOfType<MapCreator>();
        chrono = FindObjectOfType<chronoScript>();
        score = FindObjectOfType<PlayerScore>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerDeath = FindObjectOfType<PlayerDeath>();
        menuWin = creator.getLayerMenu();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            score.newScore(playerDeath.getDeath(), chrono.getChrono());
            playerMovement.switchControlable();
            chrono.setChrono(false);
            winSoundEffect.Play();
            played = true;
        }
    }

    private void Update()
    {
        if (played && !winSoundEffect.isPlaying) {
            menuWin.SetActive(true);
        }
    }
}

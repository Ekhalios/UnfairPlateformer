using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagWin : MonoBehaviour
{
    public AudioSource winSoundEffect;
    private bool played = false;
    private MapCreator creator;

    private void Start()
    {
        creator = FindObjectOfType<MapCreator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            winSoundEffect.Play();
            played = true;
        }
    }

    private void Update()
    {
        if (played && !winSoundEffect.isPlaying) {
            SceneManager.LoadScene(creator.getNextScene());
        }
    }
}

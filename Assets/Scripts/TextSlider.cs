using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        Debug.Log(AudioListener.volume);
        slider.value = AudioListener.volume * 100;
        Debug.Log(slider.value);
        setNumbertext(slider.value);
    }

    public void setNumbertext(float value)
    {
        numberText.text = value.ToString();
        float volume = value / 100f;
        AudioListener.volume = volume;
    }
}

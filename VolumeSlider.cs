using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    void Start()
    {
        Slider slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }
}

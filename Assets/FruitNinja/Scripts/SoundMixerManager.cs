using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class sound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    private static float currentMasterVol = 1f;
    private static float currentSFXVol = 1f;
    private static float currentMusicVol = 1f;

    private void Start()
    {
        if (MasterSlider != null) MasterSlider.value = currentMasterVol;
        if (sfxSlider != null) sfxSlider.value = currentSFXVol;
        if (musicSlider != null) musicSlider.value = currentMusicVol;

        SetMasterVolume(currentMasterVol);
        SetSoundFXVolume(currentSFXVol);
        SetMusicVolume(currentMusicVol);
    }

    public void SetMasterVolume(float level)
    {
        currentMasterVol = level;
        float volume = Mathf.Log10(Mathf.Max(0.0001f, level)) * 20f; 
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetSoundFXVolume(float level)
    {
        currentSFXVol = level;
        float volume = Mathf.Log10(Mathf.Max(0.0001f, level)) * 20f;
        audioMixer.SetFloat("SoundFXVolume", volume);
    }

    public void SetMusicVolume(float level)
    {
        currentMusicVol = level;
        float volume = Mathf.Log10(Mathf.Max(0.0001f, level)) * 20f;
        audioMixer.SetFloat("MusicVolume", volume);
    }
}
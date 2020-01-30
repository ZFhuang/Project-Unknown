using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    //SerializeField part: saving some simple params
    [SerializeField]
    private AudioMixer mainAudioMixer;
    [SerializeField]
    private GameObject volumeSlider;
    [SerializeField]
    private GameObject speedSlider;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float mainVolume;

    // Start is called before the first frame update
    public void Awake()
    {
        loadOptions();
    }

    //Save settings
    public void saveOptions()
    {
        Debug.Log("Save options!");
        PlayerPrefs.Save();
    }

    //Load settings
    public void loadOptions()
    {
        //check if mainVolume existed and load
        if (!PlayerPrefs.HasKey("mainVolume"))
        {
            Debug.Log("init mainVolume!");
            PlayerPrefs.SetFloat("mainVolume", -20f);
            mainVolume = -20f;
        }
        else if (PlayerPrefs.HasKey("mainVolume"))
        {
            Debug.Log("load mainVolume!");
            mainVolume = PlayerPrefs.GetFloat("mainVolume");
        }
        mainAudioMixer.SetFloat("MainMusicVolume", mainVolume);
        //set volume slider
        volumeSlider.GetComponent<Slider>().value = (mainVolume + 40) * 2;

        //check if speed existed and load
        if (!PlayerPrefs.HasKey("speed"))
        {
            Debug.Log("init speed!");
            PlayerPrefs.SetFloat("speed", 0.5f);
            speed = 0.5f;
        }
        else if (PlayerPrefs.HasKey("speed"))
        {
            Debug.Log("load speed!");
            speed = PlayerPrefs.GetFloat("speed");
        }
        //set speed slider
        speedSlider.GetComponent<Slider>().value = speed * 100;
    }

    //Reset settings
    public void resetOptions()
    {
        Debug.Log("Reset options!");
        PlayerPrefs.SetFloat("mainVolume", -20f);
        mainVolume = -20f;
        mainAudioMixer.SetFloat("MainMusicVolume", mainVolume);
        volumeSlider.GetComponent<Slider>().value = (mainVolume + 40) * 2;
        PlayerPrefs.SetFloat("speed", 0.5f);
        speed = 0.5f;
        speedSlider.GetComponent<Slider>().value = speed * 100;
    }

    //Set mainVolume's value
    public void setVolume(float input)
    {
        //Remember to find out and set 
        //the real min volume and max volume from -40db to 10db
        mainVolume = input * 0.5f - 40;
        mainAudioMixer.SetFloat("MainMusicVolume", mainVolume);
        PlayerPrefs.SetFloat("mainVolume", mainVolume);

        //Mute the sound
        if (input == 0)
        {
            mainVolume = -80;
            mainAudioMixer.SetFloat("MainMusicVolume", mainVolume);
            PlayerPrefs.SetFloat("mainVolume", mainVolume);
        }
    }

    public void setSpeed(float input)
    {
        //Change the move speed
        speed = input / 100;
        PlayerPrefs.SetFloat("speed", speed);
    }
}

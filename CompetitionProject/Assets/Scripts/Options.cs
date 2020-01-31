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
        PlayerPrefs.SetFloat("mainVolume", mainVolume);
        PlayerPrefs.SetFloat("speed", speed);
        Debug.Log("Save options!");
        PlayerPrefs.Save();
    }

    //Load settings
    public void loadOptions()
    {
        //check if mainVolume existed and load
        if (!PlayerPrefs.HasKey("mainVolume"))
        {
            PlayerPrefs.SetFloat("mainVolume", -20f);
            mainVolume = -20f;
            Debug.Log(this.gameObject + " init mainVolume: " + mainVolume);
        }
        else if (PlayerPrefs.HasKey("mainVolume"))
        {
            mainVolume = PlayerPrefs.GetFloat("mainVolume");
            Debug.Log(this.gameObject + " load mainVolume: " + mainVolume);
        }
        mainAudioMixer.SetFloat("MainMusicVolume", mainVolume);
        //set volume slider
        volumeSlider.GetComponent<Slider>().value = (mainVolume + 40) * 2;

        //check if speed existed and load
        if (!PlayerPrefs.HasKey("speed"))
        {
            PlayerPrefs.SetFloat("speed", 0.5f);
            speed = 0.5f;
            Debug.Log(this.gameObject + " init speed: " + speed);
        }
        else if (PlayerPrefs.HasKey("speed"))
        {
            speed = PlayerPrefs.GetFloat("speed");
            Debug.Log(this.gameObject + " load speed: " + speed);
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

        //Mute the sound
        if (input == 0)
        {
            mainVolume = -80;
            mainAudioMixer.SetFloat("MainMusicVolume", mainVolume);
        }
    }

    public void setSpeed(float input)
    {
        //Change the move speed
        speed = input / 100;
    }
}

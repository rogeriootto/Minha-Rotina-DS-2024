using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButtonToggle : MonoBehaviour
{
    private static MusicButtonToggle instance;
    private Sprite musicOnImage;
    public Button button;
    public Sprite musicOffImage;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        musicOnImage = button.image.sprite;

        // Set initial button image
        UpdateButtonImage();
    }

    public void OnButtonClick()
    {
        audioManager.ToggleMusic();
        UpdateButtonImage();
    }

    void UpdateButtonImage()
    {
        if (PlayerPrefs.GetInt("MusicOn", 1) == 1)
        {
            button.image.sprite = musicOnImage;
        }
        else
        {
            button.image.sprite = musicOffImage;
        }
    }
}

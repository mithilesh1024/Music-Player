using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playButton : MonoBehaviour
{
    [SerializeField]
    private Sprite playImage;
    [SerializeField]
    private Sprite pauseImage;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Button _forward;
    [SerializeField]
    private Button _backward;
    public bool playing;
    public playMusic music;
    private slider musicSlider;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.Find("GameObject").GetComponent<playMusic>();
        musicSlider = GameObject.Find("Slider").GetComponent<slider>();
        playing = false;
        _button.image.sprite = playImage;
        _button.onClick.AddListener(() => {
            changePlayPauseIcon();
        });
        _forward.onClick.AddListener(() =>
        {
            Move10sForward();
        });
        _backward.onClick.AddListener(() =>
        {
            Move10sBackword();
        });
    }

    private void Move10sForward()
    {
        if(music.audioSource.clip.length - music.audioSource.time <= 10)
        {
            music.audioSource.time = music.audioSource.clip.length;
            return;
        }
        if (!music.audioSource.isPlaying)
        {
            music.audioSource.time += 10;
            musicSlider.UpdatePlaytime(music.audioSource);
        }
        music.audioSource.time += 10;
 
    }

    private void Move10sBackword()
    {
        if (music.audioSource.time < 10)
        {
            music.audioSource.time = 0;
        }
        else
        {
            music.audioSource.time -= 10;
        }
        if (!music.audioSource.isPlaying)
        {
            music.audioSource.time -= 10;
            musicSlider.UpdatePlaytime(music.audioSource);
        }
    }

    public void changePlayPauseIcon()
    {
        if (_button.image.sprite == playImage)
        {
            playing = true;
            music.playPauseSong(playing);
            _button.image.sprite = pauseImage;
        }
        else
        {
            playing = false;
            music.playPauseSong(playing);
            _button.image.sprite = playImage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (playing == true)
            {
                changePlayPauseIcon();
            }
            else
            {
                changePlayPauseIcon();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move10sBackword();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move10sForward();
        }
    }
}

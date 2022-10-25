using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // Start is called before the first frame update
    private playMusic music;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Sprite unmute;
    [SerializeField]
    private Sprite mute;
    [SerializeField]
    private Button _button;
    private bool isMute = false;
    public Text VolumeInText;


    void Start()
    {
        music = GameObject.Find("GameObject").GetComponent<playMusic>();
        musicSlider.maxValue = 1;
        musicSlider.value = 1;
        _button.onClick.AddListener(() =>
        {
            if (!isMute)
            {
                Unmute();
            }
            else
            {
                Mute();
            }
        });
        musicSlider.onValueChanged.AddListener(delegate { volumeChange(); });
    }

    private void volumeChange()
    {
        music.audioSource.volume = musicSlider.value;
        VolumeInText.text = ((int)(musicSlider.value * 100)).ToString() + "%";
        if(musicSlider.value == 0)
        {
            Mute();
        }
        else
        {
            Unmute();
        }
    }

    private void Unmute()
    {
        _button.image.sprite = unmute;
        music.audioSource.mute = false;
        isMute = true;
        if(musicSlider.value == 0)
        {
            musicSlider.value = 0.3f;
        }
    }

    private void Mute()
    {
        _button.image.sprite = mute;
        music.audioSource.mute = true;
        isMute = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}

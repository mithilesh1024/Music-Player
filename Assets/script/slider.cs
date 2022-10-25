using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    [SerializeField]
    private Text startTime;
    [SerializeField]
    private Text endTime;
    [SerializeField]
    private Slider _slider;
    int playtime;
    private playMusic playMusic;
    IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (XmlParse.musicCollection.Count == 0)
        {
            XmlParse.getData();
        }
        playMusic = GameObject.Find("GameObject").GetComponent<playMusic>();
        _slider.onValueChanged.AddListener(delegate
        {
            changeMusicTime(_slider.value);
        });
    }

    public int getSliderValue()
    {
        return (int)_slider.value;
    }

    public void changeMusicTime(float value)
    {
        playMusic.audioSource.time = value;
    }

    public void setEndTime(float length)
    {
        _slider.maxValue = length;
        length %= 3600;
        int minitues = (int)length / 60;
        length %= 60;
        int seconds = (int)length;
        endTime.text = string.Format("{0:0}:{1:00}", minitues.ToString(), seconds.ToString());
    }

    public void resetStartTime()
    {
        startTime.text = string.Format("00:00");
        _slider.value = 0;
    }

    public void startTimer(AudioSource source)
    {
        coroutine = time(source);
        if (coroutine != null)
        {
            StartCoroutine(coroutine);
        }
    }

    public void stopTimer()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    IEnumerator time(AudioSource source)
    {
        while (source.isPlaying)
        {
            playtime = (int)source.time;
            _slider.value = playtime;
            displayTime();
            if (playtime == (int)source.clip.length)
            {
                playMusic.nextSong();
            }
            yield return null;
        }
    }

    public void UpdatePlaytime(AudioSource source)
    {
        playtime = (int)source.time;
        _slider.value = playtime;
        displayTime();
    }

    public void displayTime()
    {
        int seconds = playtime % 60;
        int minutes = (playtime / 60) % 60;
        startTime.text = string.Format("{0:0}:{1:00}", minutes.ToString(), seconds.ToString());
    }

    // Update is called once per frame
    void Update()
    {
    }
}

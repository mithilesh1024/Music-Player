using TagLib.Riff;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class playMusic : MonoBehaviour
{
    public slider slider;
    public AudioSource audioSource;
    public MenuButton menu;
    public favouriteButton favourite;
    [SerializeField]
    private Button next;
    [SerializeField]
    private Button prev;
    [SerializeField]
    private Button repeat;
    public int index = 0;
    private playButton playButton;
    [SerializeField]
    private Text musicName;
    [SerializeField]
    private Text musicAlbum;
    [SerializeField]
    private Button shuffle;
    [SerializeField]
    private GameObject backGround;
    [SerializeField]
    private GameObject nextSongBackground;
    [SerializeField]
    private GameObject prevSongBackground;
    private favouriteButton favButton;
    public List<MusicClass> currentPlayingList;
    public Animator songNameAnimation;
    public Animator albumNameAnimation;
    System.Random ran = new System.Random();

    private void Awake()
    {
        if(XmlParse.musicCollection.Count == 0)
        {
            XmlParse.getData();
        }
        currentPlayingList = XmlParse.musicCollection;
    }

    private void init()
    {
        setAudio(currentPlayingList[index].clip);
        slider.setEndTime(currentPlayingList[index].clip.length);
        slider.resetStartTime();
        setMusicName();
        setAlbumName();
        setAlbumImage();
        setFavourite();
        
    }

    public void nextSong()
    {
        if (currentPlayingList.Count - 1 == index)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }
        setAllValues();
    }

    public void prevSong()
    {
        if (index == 0)
        {
            index = currentPlayingList.Count - 1;
        }
        else
        {
            index -= 1;
        }
        setAllValues();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slider = GameObject.Find("Slider").GetComponent<slider>();
        playButton = GameObject.Find("playbutton").GetComponent<playButton>();
        favButton = GameObject.Find("favouriteButton").GetComponent<favouriteButton>();
        menu = GameObject.Find("menuButton").GetComponent<MenuButton>();
        favourite = GameObject.Find("favouriteButton").GetComponent<favouriteButton>();
        favourite.AddLikedSongsToList(XmlParse.likedSong);
        init();
        next.onClick.AddListener(() => {
            nextSong();
        });

        prev.onClick.AddListener(() => {
            prevSong();
        });

        shuffle.onClick.AddListener(() => {
            shuffleSong();
        });

        repeat.onClick.AddListener(() =>
        {
            RepeatSong();
        });
    }

    

    private void shuffleSong()
    {
        XmlParse.Shuffle(currentPlayingList);
        index = ran.Next(currentPlayingList.Count);
        setAllValues();
        menu.reCreateAllSongsList();
    }

    private void setAllValues()
    {
        setMusicName();
        setAlbumImage();
        setAlbumName();
        setAudio(currentPlayingList[index].clip);
        slider.setEndTime(currentPlayingList[index].clip.length);
        playPauseSong(playButton.playing);
        slider.resetStartTime();
        setFavourite();
        menu.reCreateAllSongsList();
    }

    public void setAllValues(int idx, bool change = false)
    {
        if (change)
        {
            currentPlayingList = XmlParse.likedSong;
        }
        else
        {
            currentPlayingList = XmlParse.musicCollection;
        }
        index = idx;
        setMusicName();
        setAlbumImage();
        setAlbumName();
        setAudio(currentPlayingList[index].clip);
        slider.setEndTime(currentPlayingList[index].clip.length);
        playPauseSong(playButton.playing);
        slider.resetStartTime();
        setFavourite();
        menu.reCreateAllSongsList();
    }

    private void setFavourite()
    {
        if (currentPlayingList[index].favourite == true)
        {
            favButton.setLiked();
        }
        else
        {
            favButton.setUnLiked();
        }
    }

    void RepeatSong()
    {
        audioSource.time = 0;
    }


    private void setMusicName()
    {
        songNameAnimation.Play("songName", -1, 0f);
        musicName.text = currentPlayingList[index].name;
    }

    private void setAlbumName()
    {
        albumNameAnimation.Play("albumName", -1, 0f);
        musicAlbum.text = currentPlayingList[index].albumn;
    }
    private void setAlbumImage()
    {
        GameObject.Find("Panel").GetComponent<Image>().sprite = currentPlayingList[index].imagePath;
    }

    private void setAudio(AudioClip music)
    {  
        audioSource.clip = music;
    }

    public void playPauseSong(bool play)
    {
        if(play == true)
        {
            audioSource.Play();
            slider.startTimer(audioSource);
        }
        else
        {
            audioSource.Pause();
            slider.stopTimer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            nextSong();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            prevSong();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RepeatSong();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            shuffleSong();
        }
    }
}

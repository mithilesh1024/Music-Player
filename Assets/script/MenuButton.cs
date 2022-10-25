using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Button allSongs;
    public Button likedSongs;
    public GameObject panel;
    public Button _openButton;
    public Button _exitButton;
    private playMusic music;
    public GameObject prefab;
    public Transform allSong;
    public GameObject allSongsList;
    public GameObject likedSongsList;
    public GameObject visual;
    public GameObject dance;
   // public Transform likedSong;
    private bool panelOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.Find("GameObject").GetComponent<playMusic>();
        panel.SetActive(false);
        dance.SetActive(true);
        visual.SetActive(true);
        allSongsList.SetActive(true);
        likedSongsList.SetActive(false);
        _openButton.onClick.AddListener(() =>
        {
            disablePanel(true);
            
        });
        _exitButton.onClick.AddListener(() =>
        {
            disablePanel(false);
        });
        allSongs.onClick.AddListener(() =>
        {
            allSongsList.SetActive(true);
            likedSongsList.SetActive(false);
        });
        likedSongs.onClick.AddListener(() =>
        {
            allSongsList.SetActive(false);
            likedSongsList.SetActive(true);
        });
       
        foreach (MusicClass selectedMusic in XmlParse.musicCollection)
        {
            GameObject newSong = Instantiate(prefab, allSong);
            newSong.transform.GetChild(0).GetComponent<Text>().text = selectedMusic.name;
            newSong.transform.GetChild(1).GetComponent<Text>().text = selectedMusic.albumn;
            newSong.transform.GetChild(2).GetComponent<Image>().sprite = selectedMusic.imagePath;
            if (XmlParse.musicCollection[music.index].name == selectedMusic.name)
            {
                ColorBlock cb = newSong.transform.GetComponent<Button>().colors;
                cb.normalColor = Color.blue;
                newSong.transform.GetComponent<Button>().colors = cb;

            }
            newSong.transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                changeSong(selectedMusic.name, "All Songs");
                panel.SetActive(false);
                panelOpen = false;
                reCreateAllSongsList();
            });
        }
    }

    

    public void disablePanel(bool value)
    {
        panel.SetActive(value);
        panelOpen = value;
        visual.SetActive(!value);
        dance.SetActive(!value);
    }

    void changeSong(string selectedMusic, string playlist)
    {
        if(playlist == "All Songs")
        {
            var idx = XmlParse.musicCollection.FindIndex(m => m.name == selectedMusic);

            music.setAllValues(idx, false);
        }
        else
        {
            var idx = XmlParse.likedSong.FindIndex(m => m.name == selectedMusic);
            music.setAllValues(idx, true);
        }
    }

    public void reCreateAllSongsList()
    {
        foreach (Transform child in allSong.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (MusicClass selectedMusic in XmlParse.musicCollection)
        {
            GameObject newSong = Instantiate(prefab, allSong);
            newSong.transform.GetChild(0).GetComponent<Text>().text = selectedMusic.name;
            newSong.transform.GetChild(1).GetComponent<Text>().text = selectedMusic.albumn;
            newSong.transform.GetChild(2).GetComponent<Image>().sprite = selectedMusic.imagePath;
            if (XmlParse.musicCollection[music.index].name == selectedMusic.name)
            {
                ColorBlock cb = newSong.transform.GetComponent<Button>().colors;
                cb.normalColor = Color.blue;
                newSong.transform.GetComponent<Button>().colors = cb;

            }
            newSong.transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                changeSong(selectedMusic.name, "All Songs");
                panel.SetActive(false);
                panelOpen = false;
                reCreateAllSongsList();
            });
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            panelOpen = !panelOpen;
            panel.SetActive(panelOpen);
        }
    }
}

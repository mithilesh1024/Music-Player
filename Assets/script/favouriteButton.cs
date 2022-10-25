using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class favouriteButton : MonoBehaviour
{
    [SerializeField]
    private Sprite liked;
    [SerializeField]
    private Sprite unLiked;
    [SerializeField]
    public Button likedButton;
    private playMusic playMusic;
    public GameObject prefab;
    public Transform location;
    public MenuButton menu;
    public List<GameObject> list; 

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("menuButton").GetComponent<MenuButton>();
        playMusic = GameObject.Find("GameObject").GetComponent<playMusic>();
        likedButton.onClick.AddListener(() =>
        {
            
            if (likedButton.image.sprite == liked)
            {
                likedButton.image.sprite = unLiked;
                removeLike(XmlParse.musicCollection[playMusic.index].name);
            }
            else
            {
                addLike(XmlParse.musicCollection[playMusic.index].name);
                likedButton.image.sprite = liked;
            }
        });
    }

    public void setLiked()
    {
        likedButton.image.sprite = liked;
    }

    public void setUnLiked()
    {
        likedButton.image.sprite = unLiked;
    }

    private void addLike(string selectedMusic)
    {
        var idx = XmlParse.musicCollection.FindIndex(m => m.name == selectedMusic);
        XmlParse.addLikedSong(XmlParse.musicCollection[idx].songPath);
        XmlParse.musicCollection[idx].favourite = true;
        updateList("add", idx);
    }

    private void removeLike(string selectedMusic)
    {
        var idx = XmlParse.musicCollection.FindIndex(m => m.name == selectedMusic);
        XmlParse.removeLikedSong(XmlParse.musicCollection[idx].songPath);
        XmlParse.musicCollection[idx].favourite = false;
        var removeIndex = list.FindIndex(i => i.name == selectedMusic);
        updateList("remove", removeIndex);
    }

    public void AddLikedSongsToList(List<MusicClass> list)
    {
        foreach(MusicClass music in list)
        {
            GameObject newSong = Instantiate(prefab, location);
            newSong.transform.GetChild(0).GetComponent<Text>().text = music.name;
            newSong.transform.GetChild(1).GetComponent<Text>().text = music.albumn;
            newSong.transform.GetChild(2).GetComponent<Image>().sprite = music.imagePath;
            newSong.transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                changeSong(music.name);
                menu.disablePanel(false);
            });
        }
    }

    private void updateList(string value, int idx)
    {
        if(value == "add")
        {
            GameObject newSong = Instantiate(prefab, location);
            newSong.transform.GetChild(0).GetComponent<Text>().text = XmlParse.musicCollection[idx].name;
            newSong.transform.GetChild(1).GetComponent<Text>().text = XmlParse.musicCollection[idx].albumn;
            newSong.transform.GetChild(2).GetComponent<Image>().sprite = XmlParse.musicCollection[idx].imagePath;
            newSong.transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                changeSong(XmlParse.musicCollection[idx].name);
                menu.disablePanel(false);
            });
        }
        else
        {
            foreach (Transform child in location.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (MusicClass music in XmlParse.musicCollection)
            {
                if (music.favourite)
                {
                    GameObject newSong = Instantiate(prefab, location);
                    newSong.transform.GetChild(0).GetComponent<Text>().text = music.name;
                    newSong.transform.GetChild(1).GetComponent<Text>().text = music.albumn;
                    newSong.transform.GetChild(2).GetComponent<Image>().sprite = music.imagePath;
                    newSong.transform.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        changeSong(music.name);
                        menu.disablePanel(false);
                    });
                }
            }
        }
    }

    void changeSong(string selectedMusic)
    {
        var idx = XmlParse.likedSong.FindIndex(m => m.name == selectedMusic);
        playMusic.setAllValues(idx, true);
    }
}

using System;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DownloadFile : MonoBehaviour
{
    public playMusic playmusic;
    public InputField input;
    public Button submit;
    public Button addSong;
    public Button removeSong;
    public Button exit;
    public GameObject panel;
    private string url;
    private string location = @"C:\New folder (2)\Music Player\Assets\music\";
    public GameObject visual;
    public GameObject dance;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("sd");
        panel.SetActive(false);
        playmusic = GameObject.Find("GameObject").GetComponent<playMusic>();
        addSong.onClick.AddListener(() =>
        {
            Debug.Log("open");
            panel.SetActive(true);
            visual.SetActive(false);
            dance.SetActive(false);
        });
        exit.onClick.AddListener(() =>
        {
            Debug.Log("exit add song");
            panel.SetActive(false);
            visual.SetActive(true);
            dance.SetActive(true);
        });
        input.onValueChanged.AddListener((string value) =>
        {
            url = value;
        });
        removeSong.onClick.AddListener(() =>
        {
            removeSongFromList();
        });
        submit.onClick.AddListener(() =>
        {
            bool isUri = Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
            
                string name = "";
                string[] list = url.Split('/');
                string[] noExpt = list[list.Length - 1].Split('.');
                for(int i=0; i < noExpt.Length-1; i++)
                    name += Regex.Replace(noExpt[i], @"[^0-9a-zA-Z]+", " ");
                name += ".mp3";
                var client = new WebClient();
                Debug.Log("url " + url);
                Debug.Log("location " + location + name);
                client.Headers.Add("User-Agent: Other");
                try
                {
                    client.DownloadFile(new Uri(url), location + name);
                    Debug.Log(name);
                    XmlParse.AddNewSong(location + name);
                }catch(Exception e)
                {
                    Debug.Log("error " + e);
                }
                playmusic.setAllValues(XmlParse.musicCollection.Count-1);
                panel.SetActive(false);
                visual.SetActive(true);
                dance.SetActive(true);
            
        });
    }

    void removeSongFromList()
    {
        var idx = XmlParse.musicCollection.FindIndex(m => m.name == XmlParse.musicCollection[playmusic.index].name);
        XmlParse.DeleteSong(idx);
    }
}

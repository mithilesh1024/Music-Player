using System.IO;
using UnityEngine;

public class GetHandData : MonoBehaviour
{
    string currentGesture = "";
    string previousGesture = "";
    bool init = false;
    public playButton music;

    private void Start()
    {
        music = GameObject.Find("playbutton").GetComponent<playButton>();
    }

    private void Update()
    {
        FileStream fs = File.Open("C:\\Users\\mkapadi\\Music Player\\Assets\\opencv\\output.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        string fileContents;
        using (StreamReader reader = new StreamReader(fs))
        {
            fileContents = reader.ReadToEnd();
        }
        if (!init)
        {
            init = true;
            previousGesture = fileContents;
        }
        currentGesture = fileContents;
        if(previousGesture != currentGesture)
        {
            if (music.playing)
            {
                if (currentGesture == "peace")
                {
                    Debug.Log("pause");
                    music.changePlayPauseIcon();
                }
            }
            if (!music.playing)
            {
                if (currentGesture == "thumbs up")
                {
                    Debug.Log("play");
                    music.changePlayPauseIcon();
                }
            }
            currentGesture = previousGesture;
        }
    }

}
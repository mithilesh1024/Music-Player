using UnityEngine;

public class MusicClass
{

    public string name;
    public string albumn;
    public string songPath;
    public Sprite imagePath;
    public AudioClip clip;
    public bool favourite;

    public MusicClass(string name,string albumn,Sprite imagePath,string song, AudioClip clip, bool fav)
    {
        this.name = name;   
        this.albumn = albumn;   
        this.imagePath = imagePath;
        this.songPath = song;
        this.clip = clip;
        this.favourite = fav;
    }
}

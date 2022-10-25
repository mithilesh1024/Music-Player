using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TagLib;
using UnityEngine;

static class XmlParse
{

    private static string location = "E:\\Music Player\\Assets\\script\\music.xml";
    public static List<MusicClass> musicCollection = new List<MusicClass>();
    public static List<MusicClass> likedSong = new List<MusicClass>();
    private static XmlDocument doc = new XmlDocument();
    static System.Random ran = new System.Random();
    static XDocument xml = XDocument.Load(location);
    static playMusic music = GameObject.Find("GameObject").GetComponent<playMusic>();
    static IEnumerable<XElement> elements = xml.Root
                          .Elements("music")
                          .Elements("song");

    public static void getData()
    {
        doc.Load(location);

        XmlNode root = doc.DocumentElement;
        XmlNodeList nodes = root.SelectNodes("/musics/music");

        foreach (XmlNode node in nodes)
        {
            string song = node["song"].InnerText;
            string fav = node["liked"].InnerText;
            extractData(song,fav);
        }
    }

    static void extractData(string song, string fav)
    {
        Sprite trueImg;
        var tfile = TagLib.File.Create(@song);
        string name;
        if (tfile.Tag.Title == null)
        {
            name = "Unknown Song Name";
        }
        else
        {
            name = tfile.Tag.Title;
        }
        string album;
        if (tfile.Tag.Album == null)
        {
            album = "Unknown Album Name";
        }
        else
        {
            album = tfile.Tag.Album;
        }
        IPicture[] picture = tfile.Tag.Pictures;
        if (picture.Length != 0)
        {
            byte[] imgByte = picture[0].Data.Data;
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgByte);

            trueImg = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2());
        }
        else
        {
            trueImg = null;
        }
        AudioClip audioClip = LoadAudio(song);
        MusicClass temp = new MusicClass(name, album, trueImg, song, audioClip, bool.Parse(fav));
        musicCollection.Add(temp);
        if (fav == "true")
        {
            likedSong.Add(temp);
        }
    }

    private static AudioClip LoadAudio(string path)
    {
        string fullPath = "file://" + path;
        try
        {
            WWW url = new WWW(fullPath);
            AudioClip audioClip = url.GetAudioClip(false, true);
            return audioClip;
        }
        catch (Exception e)
        {
            Debug.Log("error " + e);
            return null;
        }
    }

    public static void addLikedSong(string t)
    {
         XElement element = xml.Root
                          .Elements("music")
                          .Elements("song")
                          .Where(x => x.Value == t)
                          .SingleOrDefault();
            foreach (XElement child in elements)
            {
                if (child.Value == t)
                {
                    element = child;
                    break;
                }
            }
        XElement c = element.Parent;
        c.Element("liked").Value = "true";
        xml.Save(location);
  
    }

    public static void removeLikedSong(String t)
    {
        XElement element = xml.Root
                          .Elements("music")
                          .Elements("song")
                          .Where(x => x.Value == t)
                          .SingleOrDefault();
            foreach (XElement child in elements)
            {
                if (child.Value == t)
                {
                    element = child;
                    break;
                }
            }
        XElement c = element.Parent;
        c.Element("liked").Value = "false";
        xml.Save(location);
    }

    public static void AddNewSong(string path)
    {
        XmlNode music = doc.CreateElement("music");
        XmlNode song = doc.CreateElement("song");
        song.InnerText = path;
        XmlNode liked = doc.CreateElement("liked");
        liked.InnerText = "false";
        music.AppendChild(song);
        music.AppendChild(liked);
        doc.DocumentElement.AppendChild(music);
        doc.Save(location);
        extractData(path, "false");
    }

    public static void DeleteSong(int idx)
    {
        string t = "";
        XElement element = xml.Root
                          .Elements("music")
                          .Elements("song")
                          .Where(x => x.Value == t)
                          .SingleOrDefault();
        foreach (XElement child in elements)
        {
            if (child.Value == XmlParse.musicCollection[idx].songPath)
            {
                element = child;
                break;
            }
        }
        XElement c = element.Parent;
        c.Remove();
        xml.Save(location);
        if (likedSong.Any(item => item.name == musicCollection[idx].name))
        {
            var likedSongIdx = likedSong.FindIndex(m => m.name == musicCollection[idx].name);
            likedSong.RemoveAt(likedSongIdx);
        }
        musicCollection.RemoveAt(idx);
        if(idx + 1 > musicCollection.Count - 1)
        {
            music.setAllValues(0);
        }
        else
        {
            music.setAllValues(idx + 1);
        }
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ran.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}
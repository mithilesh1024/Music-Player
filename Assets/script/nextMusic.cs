using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextMusic : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> musics;
    [SerializeField]
    private Button next;
    [SerializeField]
    private Button prev;
    int index = 0;
    private playMusic playmusic;

    private void Awake()
    {
        playmusic = GameObject.Find("GameObject").GetComponent<playMusic>();
    }

    // Start is called before the first frame update
    void Start()
    {
        next.onClick.AddListener(() => {
            if (musics.Count == index)
            {
                index = 0;
            }
            else
            {
                index = index + 1;
            }
        });

        prev.onClick.AddListener(() =>
        {

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

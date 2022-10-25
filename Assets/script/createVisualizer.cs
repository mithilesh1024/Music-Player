using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createVisualizer : MonoBehaviour
{
    public GameObject[] cubes;
    public playMusic music;
    float minimum = 10.0f;
    float maximum = 20.0f;
    float duration = 5.0f;
    float startTime;

    private void Start()
    {
        music = GameObject.Find("GameObject").GetComponent<playMusic>();
        startTime = Time.time;
    }

    private void Update()
    {
       // Debug.Log(AudioPeer._audioBand[7]);
        if (music.audioSource.isPlaying)
        {
            float t = (Time.time - startTime) / duration;
            for (int i = 0; i < cubes.Length; i++)
            {
                if (!double.IsNaN(AudioPeer._audioBand[i]))
                {
                    cubes[i].transform.localScale = new Vector3(cubes[i].transform.localScale.x,Mathf.SmoothStep(0, AudioPeer._audioBand[i] * 1000,t), cubes[i].transform.localScale.z);
                }
            }
        }
    }
}

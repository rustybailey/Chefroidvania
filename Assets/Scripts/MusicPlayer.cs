using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// One-off script to play music in a scene without zones
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] string musicTrack;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic(musicTrack);
    }
}

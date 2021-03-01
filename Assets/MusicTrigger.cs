using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] string track1;
    [SerializeField] string track2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayDesiredMusic();
        }
    }

    private void PlayDesiredMusic()
    {
        if (track1 == null || track2 == null)
        {
            Debug.Log("Both tracks must be selected!");
            return;
        }

        Sound currentTrack = AudioManager.instance.GetCurrentlyPlayingMusic();
        if (currentTrack == null)
        {
            AudioManager.instance.FadeInTrack(track2, 1f);
            return;
        }

        string fadeOutTrack = currentTrack.GetName() == track1 ? track1 : track2;
        string fadeInTrack = currentTrack.GetName() == track1 ? track2 : track1;
        AudioManager.instance.CrossFadeBetweenTwoTracks(fadeOutTrack, fadeInTrack, 1f);
    }
}

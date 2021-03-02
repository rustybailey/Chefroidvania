using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] string targetTrackName;

    private float fadeDuration = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayDesiredMusic();
        }
    }

    private void PlayDesiredMusic()
    {
        if (targetTrackName == null)
        {
            Debug.Log("A target track must be selected");
            return;
        }

        Sound currentTrack = AudioManager.instance.GetCurrentlyPlayingMusic();
        if (currentTrack == null)
        {
            AudioManager.instance.FadeInTrack(targetTrackName, 1f);
            return;
        }

        if (currentTrack.GetName() == targetTrackName)
        {
            Debug.Log("Target track is already playing");
            return;
        }

        AudioManager.instance.CrossFadeBetweenTwoTracks(currentTrack.GetName(), targetTrackName, fadeDuration);
    }
}

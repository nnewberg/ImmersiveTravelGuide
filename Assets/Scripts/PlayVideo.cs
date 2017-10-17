using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class PlayVideo : MonoBehaviour {

    public VideoClip Clip;
    public string URL;
    public AudioSource AudioSource;
    private VideoPlayer videoPlayer;

    void Awake()
    {
        //Subscribe to pickup and dropping events on this object
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();
        obj.onPickUp.AddListener(Play);
        obj.onDetachFromHand.AddListener(Stop);

        videoPlayer = GetComponent<VideoPlayer>();
        if (URL.Length > 0)
        {
            videoPlayer.url = URL;
            videoPlayer.source = VideoSource.Url;
        }else
        {
            videoPlayer.clip = Clip;
            videoPlayer.source = VideoSource.VideoClip;

        }
        videoPlayer.SetTargetAudioSource(0, AudioSource);

        //Creating a 'psuedo-thumbnail'
        videoPlayer.frame = (long) videoPlayer.frameCount / 2L;

        //KINDA SKETCH
        videoPlayer.Play();
        videoPlayer.Pause();
    }

    private void Play()
    {
        videoPlayer.frame = 0L;
        videoPlayer.Play();
    }

    private void Stop()
    {
        videoPlayer.frame = (long)videoPlayer.frameCount / 2L;
        videoPlayer.Pause();
    }


}

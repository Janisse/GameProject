using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JAudioManager : MonoBehaviour
{
    #region Properties
    public AudioListener listener = null;
    public AudioClip[] musicPlaylist = null;

    private List<AudioSource> audioSrcList = new List<AudioSource>();
    private AudioSource audioSrcMusic = null;
    #endregion

    #region Object Management
    void Start()
    {
        audioSrcMusic = gameObject.AddComponent<AudioSource>();
        PlayMusic();
    }
    #endregion

    #region Audio Methods
    internal void PlaySound2D(AudioClip audioFile, float volume = 1f, bool isLoop = false)
    {
        AudioSource audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrcList.Add(audioSrc);
        audioSrc.volume = volume;
        audioSrc.clip = audioFile;
        audioSrc.loop = isLoop;
        audioSrc.Play();
        if (!isLoop)
            StartCoroutine(CheckEndSound(audioSrc));
    }

    internal void PlaySound3D(AudioClip audioFile, GameObject entity, float volume = 1f, bool isLoop = false, float pitch = 1f)
    {
        AudioSource audioSrc = entity.AddComponent<AudioSource>();
        audioSrc.pitch = pitch;
        audioSrc.volume = volume;
        audioSrcList.Add(audioSrc);
        audioSrc.clip = audioFile;
        audioSrc.loop = isLoop;
        audioSrc.Play();
        if (!isLoop)
            StartCoroutine(CheckEndSound(audioSrc));
    }

    internal void PlayMusic(AudioClip audioFile)
    {
        audioSrcMusic.Stop();
        audioSrcMusic.clip = audioFile;
        audioSrcMusic.loop = true;
        audioSrcMusic.Play();
    }

    internal void PlayMusic()
    {
        audioSrcMusic.Stop();
        audioSrcMusic.volume = 0.15f;
        if (musicPlaylist.Length > 0)
        {
            audioSrcMusic.clip = musicPlaylist[Random.Range(0, musicPlaylist.Length)];
            audioSrcMusic.loop = false;
            audioSrcMusic.Play();
            StartCoroutine(CheckEndMusic());
        }
    }

    IEnumerator CheckEndSound(AudioSource audioSrc)
    {
        bool whileDo = true;
        while (whileDo)
        {
            if (audioSrc != null)
            {
                if (audioSrc.isPlaying)
                {
                    yield return null;
                }
                else
                {
                    whileDo = false;
                }
            }
            else
                whileDo = false;
        }
        audioSrcList.Remove(audioSrc);
        Destroy(audioSrc);
    }

    IEnumerator CheckEndMusic()
    {
        while (audioSrcMusic.isPlaying)
            yield return null;
        PlayMusic();
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHandler : MonoBehaviour 
{
    public TMP_InputField inputField;

    private string songName;
    private string lyrics;

    public void GetSongName()
    {
        songName = inputField.text;
        SongHolder.Instance.songName = songName;
        Debug.Log("Song name received: " + songName);
    }

    public void GetLyrics()
    {
        lyrics = inputField.text;
        SongHolder.Instance.lyrics = lyrics;
        Debug.Log("Lyrics received: " + lyrics);
    }
}

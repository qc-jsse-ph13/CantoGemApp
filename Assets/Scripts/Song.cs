using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Song : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private string songName;

    public void SetText(string filePath)
    {
        string fileName = Path.GetFileName(filePath);
        songName = Path.GetFileNameWithoutExtension(fileName);

        tmp.text = songName;
    }

    public void Playback()
    {
        SongHolder.Instance.songName = songName;
    }
}

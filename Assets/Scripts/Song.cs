using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Song : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public GameObject prefab;

    private string songName;

    public void SetText(string filePath)
    {
        string fileName = Path.GetFileName(filePath);
        songName = Path.GetFileNameWithoutExtension(fileName);

        tmp.text = songName;
    }

    public void Delete()
    {
        string mp3Path = Application.persistentDataPath + "/" + this.songName + ".mp3";
        string jsonPath = Application.persistentDataPath + "/" + this.songName + ".json";

        deleteFile(mp3Path);
        deleteFile(jsonPath);

        Destroy(prefab);
    }

    public void Playback()
    {
        SongHolder.Instance.songName = songName;
    }

    private void deleteFile(string path)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, path);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            Debug.Log("File deleted: " + fullPath);
        }
        else
        {
            Debug.LogWarning("File not found: " + fullPath);
        }
    }
}

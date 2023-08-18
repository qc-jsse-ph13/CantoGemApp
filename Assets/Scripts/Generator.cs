using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;
using System;
using UnityEngine.Networking;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;
using TMPro;
using File = System.IO.File;

public class Generator : MonoBehaviour
{
    public TextMeshProUGUI tmp;
   
    private WebHandler webHandler;

    private string songName;
    private string lyrics;


    void Start()
    {
        string user_id = PlayerPrefs.GetString("user_id");

        if (string.IsNullOrEmpty(user_id))
        {
            throw new Exception("No user id found!");
        }

        webHandler = new WebHandler(user_id, this);

        songName = SongHolder.Instance.songName;
        lyrics = SongHolder.Instance.lyrics;

        webHandler.startGenerate(lyrics, songName);
    }

    void Update()
    {
        if (webHandler.hasFinishedDownloading())
        {
            if (webHandler.getStatusCode() != 200)
            {
                if(webHandler.getStatusCode() == 409)
                {
                    SceneManager.LoadScene("Home Screen", LoadSceneMode.Single);
                    Init.ResetUserID();
                    Debug.Log("Re-generating user-id");
                }
                webHandler.Reset();
                return;
            }

            SceneManager.LoadScene("Save Screen", LoadSceneMode.Single);
            saveResult(songName, lyrics, webHandler.getResult());

            webHandler.Reset();

        } else
        {
            string percentageText = webHandler.getProgress();
            tmp.text = "Progress: " + percentageText;
            transform.localScale = new Vector3(float.Parse(percentageText.TrimEnd('%')) / 100f, 1f, 1f);
        }

    }

    private void saveResult(string songName, string lyrics, string result)
    {
        string json = @$"{{
            ""songName"": ""{songName}"",
            ""lyrics"": ""{lyrics}"",
            ""result"": ""{result}"",
            ""tempo"": 120
        }}";

        string savePath = Application.persistentDataPath + "/" + songName + ".json";
        File.WriteAllText(savePath, json);
    }


}



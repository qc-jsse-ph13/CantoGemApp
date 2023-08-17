using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;
using System;
using UnityEngine.Networking;
using UnityEngine.Windows;
using System.IO;
using File = System.IO.File;
using static UnityEditor.Progress;

public class Playback : MonoBehaviour
{
    public AudioSource audioSource;

    public int tempo = 120;


    private void Start()
    {
        string songName = SongHolder.Instance.songName;
        string mp3Path = Application.persistentDataPath + "/" + songName + ".mp3";
        string jsonPath = Application.persistentDataPath + "/" + songName + ".json";

        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            SongData songData = JsonUtility.FromJson<SongData>(json);

            // Access the loaded data
            string loadedSongName = songData.songName;
            string loadedLyrics = songData.lyrics;
            string loadedResult = songData.result;

            callAnimation(loadedLyrics, loadedResult);
            StartCoroutine(LoadAndPlayAudio(mp3Path));
        }
        else
        {
            Debug.LogError("File does not exist: " + jsonPath);
        }

    }

    private class SongData
    {
        public string songName;
        public string lyrics;
        public string result;

        public SongData(string songName, string lyrics, string result)
        {
            this.songName = songName;
            this.lyrics = lyrics;
            this.result = result;
        }
    }


    private void callAnimation(string lyrics, string resultSong)
    {
        // format result
        string[] elements = resultSong.Split('|');
        int[] pitches = new int[elements.Length];
        int[] durations = new int[elements.Length];

        for (int i = 0; i < elements.Length; i++)
        {
            string[] values = elements[i].Split(',');
            pitches[i] = int.Parse(values[0]);
            durations[i] = int.Parse(values[1]);
        }

        StringBuilder lyricsBuilder = new StringBuilder(lyrics);

       
        for (int i = 0, j = 0; i < elements.Length - 1; i++)
        {
            if (pitches[i] == 0)
            {
                lyricsBuilder.Insert(i + j, " ");
                j++;
            }
        }

        float[] actualDurationArray = new float[durations.Length];

        for (int i = 0; i < durations.Length; i++)
        {
            actualDurationArray[i] = 15f / tempo * durations[i];
        }

        string formattedLyrics = lyricsBuilder.ToString().Replace("|", ",");

        KaraokeAnimation.StartAnimation(formattedLyrics, actualDurationArray);
    }

    private IEnumerator LoadAndPlayAudio(string path)
    {
        audioSource = GetComponent<AudioSource>();

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else
            {
                Debug.LogError("Failed to load audio clip: " + www.error);
            }
        }
    }

}

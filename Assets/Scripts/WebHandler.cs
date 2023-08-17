using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebHandler
{
    private const String server_address = "http://8.222.130.100";
    private string user_id;
    
    private string progress;
    private bool finishedDownloading;
    private int statusCode;

    private string result;

    private MonoBehaviour mono;


    // http://8.222.130.100/<id>/generate
    // http://8.222.130.100/<id>/progress
    // http://8.222.130.100/<id>/get-mp3
    // http://8.222.130.100/<id>/changetempo/<newtempo>


    public WebHandler(string user_id, MonoBehaviour mono)
    {
        this.user_id = user_id;
        this.mono = mono;
    }

    public void startGenerate(string lyrics, string songName)
    {
        Reset();
        mono.StartCoroutine(generate(lyrics, songName));
    }

    public void tryChangeTempo(string newTempo)
    {
        Reset();
        mono.StartCoroutine(changeTempo(newTempo));
    }

    public string getProgress()
    {
        return progress;
    }

    public bool hasFinishedDownloading()
    {
        return finishedDownloading;
    }

    public int getStatusCode()
    {
        return statusCode;
    }

    public string getResult()
    {
        return result;
    }

    public void Reset()
    {
        progress = "0.00%";
        finishedDownloading = false;
        result = "";
        statusCode = -1;
    }


    private IEnumerator generate(string lyrics, string songName, string modelType = "random")
    {
        string base_url = server_address + "/" + user_id;
        string send_data_url = base_url + "/generate";
        string progress_bar_url = base_url + "/progress";
        string play_mp3_url = base_url + "/get-mp3";
        string midi_url = base_url + "/get-midi";

        string formatted_lyrics = @$"{{
            ""lyrics"": ""{lyrics}"",
            ""model_type"": ""{modelType}""
        }}";
        mono.StartCoroutine(get_progress_bar_request(progress_bar_url));
        yield return mono.StartCoroutine(postRequest(send_data_url, formatted_lyrics));

        if (statusCode == 200) {
            mono.StartCoroutine(downloadMP3(play_mp3_url, songName));
            // TODO: Implement
            // mono.StartCoroutine(downloadMIDI(midi_url, songName));
        }
    }

    private IEnumerator changeTempo(string newTempo)
    {
        string base_url = server_address + "/" + user_id;
        string change_tempo_url = base_url + "/changetempo";
        string play_mp3_url = base_url + "/get-mp3";

        string request = @$"{{""new_tempo"": {newTempo}}}";

        Debug.Log(request);

        yield return mono.StartCoroutine(postRequest(change_tempo_url, request));

        if (statusCode == 200) {
            mono.StartCoroutine(downloadMP3(play_mp3_url, SongHolder.Instance.songName));
            // TODO: Implement
            // mono.StartCoroutine(downloadMIDI(midi_url, SongHolder.Instance.songName));
        }
    }


    private IEnumerator downloadMP3(string url, string songName)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error downloading MP3: " + www.error);
                yield break;
            }

            Debug.Log("Loaded, dataset path = " + Application.persistentDataPath);

            string filePath = Application.persistentDataPath + "/" + songName + ".mp3";
            System.IO.File.WriteAllBytes(filePath, www.downloadHandler.data);

            finishedDownloading = true;
        }
    }



    private IEnumerator get_progress_bar_request(string uri)
    {

        yield return new WaitForSeconds(1f); // wait for file creation

        float prev_percentage = 0;

        while (true)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            uwr.timeout = 1000;

            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                string text = uwr.downloadHandler.text;
                Debug.Log(text.TrimEnd('%'));
                float percentage = float.Parse(text.TrimEnd('%'));

                if (percentage < prev_percentage)
                {
                    progress = text;
                    yield break;
                }

                progress = text;
                prev_percentage = percentage;
            }

            yield return new WaitForSeconds(1f);
        }

    }

    private IEnumerator postRequest(string uri, string jsonData) 
    {
        UnityWebRequest uwr = UnityWebRequest.Put(uri, jsonData);
        uwr.timeout = 1000;
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();
        statusCode = (int)uwr.responseCode;

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + uwr.result);
        }
        else
        {
            statusCode = 200;
            result = uwr.downloadHandler.text;
            Debug.Log("Received: " + result);
        }
    }

}

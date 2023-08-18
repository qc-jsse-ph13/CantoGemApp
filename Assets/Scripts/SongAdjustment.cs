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
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.ComponentModel;
using static Unity.Burst.Intrinsics.X86.Avx;

class SongDataType
{
	public string songName;
	public string lyrics;
	public string result;
	public int tempo = 120;


	public SongDataType(string songName, string lyrics, string result, int tempo = 120)
	{
		this.songName = songName;
		this.lyrics = lyrics;
		this.result = result;
		this.tempo = tempo;
	}
}

public class PlaybackEngine
{
	public AudioSource audioSource;
	private bool hasAudioSource = false;
	private MonoBehaviour mono;

	public PlaybackEngine(MonoBehaviour monoBehaviour) {
		mono = monoBehaviour;
	}

	public void start()
	{
		string songName = SongHolder.Instance.songName;
		string mp3Path = Application.persistentDataPath + "/" + songName + ".mp3";
		string jsonPath = Application.persistentDataPath + "/" + songName + ".json";

		if (File.Exists(jsonPath))
		{
			string json = File.ReadAllText(jsonPath);
			SongDataType songData = JsonUtility.FromJson<SongDataType>(json);

			// Access the loaded data
			string loadedSongName = songData.songName;
			string loadedLyrics = songData.lyrics;
			string loadedResult = songData.result;

			mono.StartCoroutine(LoadAndPlayAudio(mp3Path));
		}
		else
		{
			Debug.LogError("File does not exist: " + jsonPath);
		}

	}

	public IEnumerator LoadAndPlayAudio(string path)
	{
		audioSource = mono.GetComponent<AudioSource>();

		using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG))
		{
			yield return www.SendWebRequest();

			if (www.result == UnityWebRequest.Result.Success)
			{
				AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
				audioSource.clip = audioClip;
                hasAudioSource = true;
                audioSource.Play();
			}
			else
			{
				Debug.LogError("Failed to load audio clip: " + www.error);
			}
		}
	}

	public void stopAudio()
    {
		if (!hasAudioSource) return;
        hasAudioSource = false;
        audioSource.Stop();
	}
}

public class PlaybackInterface
{
	static private PlaybackEngine playback;
	static bool isReady = false;

	static public void init(MonoBehaviour mono)
	{
		if (isReady) return;
		playback = new PlaybackEngine(mono);
		isReady = true;
	}

	static public void start()
	{
		if (!isReady) return;
		playback.stopAudio();
		playback.start();
	}

	static public void stop()
	{
		if (!isReady) return;
		playback.stopAudio();
	}
}

public class SongAdjustment : MonoBehaviour
{
	public UnityEngine.UI.Slider inputField;
	public TextMeshProUGUI tempoChangeStatusText;
	private WebHandler webHandler;
	private bool BPMisProcessing = false;

	void Start()
	{
		PlaybackInterface.init(this);
		string user_id = PlayerPrefs.GetString("user_id");

		if (string.IsNullOrEmpty(user_id))
		{
			throw new Exception("No user id found!");
		}

		webHandler = new WebHandler(user_id, this);
	}

	public void attemptPlayback() {
		PlaybackInterface.start();
	}

	public void changeTempo() {
		tempoChangeStatusText.text = "Changing BPM...";
		BPMisProcessing = true;
		webHandler.tryChangeTempo(inputField.value.ToString());
	}

	void Update()
	{
		if (webHandler.hasFinishedDownloading())
		{
			BPMisProcessing = false;
			if (webHandler.getStatusCode() != 200)
			{
				webHandler.Reset();
				tempoChangeStatusText.text = "BPM change Failed";
				return;
			}

			
			PlaybackInterface.start();

			saveResult((int)inputField.value);
			webHandler.Reset();
			tempoChangeStatusText.text = "BPM change Successful";
		}
	}

	public void updateTempoString()
	{
		if (BPMisProcessing) return;
		tempoChangeStatusText.text = $"{(int)inputField.value}BPM";
	}


	private void saveResult(int newTempo)
	{

		string songName = SongHolder.Instance.songName;
		string jsonPath = Application.persistentDataPath + "/" + songName + ".json";

		if (File.Exists(jsonPath))
		{
			string json = File.ReadAllText(jsonPath);
			SongDataType songData = JsonUtility.FromJson<SongDataType>(json);

			songData.tempo = newTempo;

			File.WriteAllText(jsonPath, JsonUtility.ToJson(songData));
		}
		else
		{
			Debug.LogError("File does not exist: " + jsonPath);
		}
	}

}
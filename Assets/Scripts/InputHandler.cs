using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using static Unity.Burst.Intrinsics.X86.Avx;
using System.IO;

public class InputHandler : MonoBehaviour 
{
    public TMP_InputField inputField;
    public TextMeshProUGUI WarningTextField;

    public static bool containsIllegalCharacters;
    public static bool hasValidSongName;

    public void GetSongName()
    {
        string songName = inputField.text;

        string[] mp3Files = Directory.GetFiles(Application.persistentDataPath, "*.mp3");
        foreach (string filePath in mp3Files)
        {
            string fileName = Path.GetFileName(filePath);
            if(Path.GetFileNameWithoutExtension(fileName).Equals(songName))
            {
                WarningTextField.text = "The song name is repeated!";
                hasValidSongName = false;
                return;
            }

        }
        WarningTextField.text = "";
        hasValidSongName = true;

    
        SongHolder.Instance.songName = songName;
        Debug.Log("Song name received: " + songName);
    }

    public void GetLyrics()
    {
        string lyrics = inputField.text;

        print(containsNonChineseCharacters(lyrics));
        if (containsNonChineseCharacters(lyrics))
        {
            containsIllegalCharacters = true;
            WarningTextField.text = "Found non-Chinese words!";
            return;
        }

        WarningTextField.text = "";
        containsIllegalCharacters = false;
        

        print(containsIllegalCharacters);
        string parsedLyrics = lyrics.Replace("\n", "|").Replace(" ", ",");
        SongHolder.Instance.lyrics = parsedLyrics;

        Debug.Log("Lyrics received: " + parsedLyrics);
    }

    private bool containsNonChineseCharacters(string input)
    {
        // Regular expression pattern to match non-Chinese characters (including punctuation marks, space, and newline)
        string pattern = @"[^\u4E00-\u9FFF\u3000-\u303F\uFF00-\uFFEF\s]";

        // Check if the input string contains any non-Chinese characters using Regex.IsMatch
        bool containsNonChinese = Regex.IsMatch(input, pattern);

        return containsNonChinese;
    }
}

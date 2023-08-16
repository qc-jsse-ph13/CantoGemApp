using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHandler : MonoBehaviour 
{
    public TMP_InputField inputField;

    private string songName;
    private string lyrics;

    public static bool containsIllegalCharacters = true;

    public void GetSongName()
    {
        songName = inputField.text;
        SongHolder.Instance.songName = songName;
        Debug.Log("Song name received: " + songName);
    }

    public void GetLyrics()
    {
        lyrics = inputField.text;
        if (containsNonChineseCharacters(lyrics))
        {
            containsIllegalCharacters = true;
            Debug.Log("Only chinese characters are allowed!");
        }
        else
            containsIllegalCharacters = false;

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

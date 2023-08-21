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

    public TextAsset valid_chars;

    public static bool containsIllegalCharacters;
    public static bool hasValidSongName;

    public void GetSongName()
    {
        string songName = inputField.text;
        if (string.IsNullOrEmpty(songName))
        {
            WarningTextField.text = Texts.WARNING_TEXT_SONG_NAME_BLANK;
            hasValidSongName = false;
            return;
        }
        if (songName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
        {
            WarningTextField.text = Texts.WARNING_TEXT_SONG_NAME_INVALID;
            hasValidSongName = false;
            return;
        }

        string[] mp3Files = Directory.GetFiles(Application.persistentDataPath, "*.mp3");
        foreach (string filePath in mp3Files)
        {
            string fileName = Path.GetFileName(filePath);
            if (Path.GetFileNameWithoutExtension(fileName).Equals(songName))
            {
                WarningTextField.text = Texts.WARNING_TEXT_SONG_NAME_REPEATED;
                hasValidSongName = false;
                return;
            }
        }
        WarningTextField.text = "";
        hasValidSongName = true;

    
        SongHolder.Instance.songName = songName;
        Debug.Log("Song name received: " + songName);
    }

    const string punctuationMarks = "，。！？、：；“”‘–’⸺「-」『』《》〈〉（）【】.,!:;?'\"(){}[]";

    public void GetLyrics()
    {
        string lyrics = inputField.text;
        string parsedLyrics = lyrics;
        foreach (char punctuation in punctuationMarks) {
            parsedLyrics = parsedLyrics.Replace($"{punctuation}", ",");
        }
        parsedLyrics = parsedLyrics.Replace("\r", "").Replace("\n", "|").Replace(" ", ",");

        if (containsNonChineseCharacters(parsedLyrics.Replace("|", "").Replace(",", "")))
        {
            containsIllegalCharacters = true;
            WarningTextField.text = Texts.WARNING_TEXT_LYRICS;
            return;
        }

        WarningTextField.text = "";
        containsIllegalCharacters = false;

        SongHolder.Instance.lyrics = parsedLyrics;

        Debug.Log("Lyrics received: " + parsedLyrics);
    }

    private bool containsNonChineseCharacters(string input)
    {
        // Regular expression pattern to match non-Chinese characters (including punctuation marks, space, and newline)
        string pattern = @$"[^{valid_chars.text}]";

        // Check if the input string contains any non-Chinese characters using Regex.IsMatch
        bool containsNonChinese = Regex.IsMatch(input, pattern);

        return containsNonChinese;
    }
}

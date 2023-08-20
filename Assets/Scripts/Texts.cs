using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;

public class Texts : MonoBehaviour
{
    enum Languages {
        CHIN,
        ENG
    }

    private static Languages language = Languages.CHIN;

    public void SetLanguageToChinese()
    {
        language = Languages.CHIN;
    }
    public void SetLanguageToEnglish()
    {
        language = Languages.ENG;
    }


    public static string GENERATE
    {
        get
        {
            switch(language)
            {
                case Languages.CHIN:
                    return "生成";
                case Languages.ENG:
                    return "Generate";
            }
            return "";
        }
    }

    public static string LIBRARY
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "歌庫";
                case Languages.ENG:
                    return "Library";
            }
            return "";
        }
    }


    public static string PLAY
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "播放";
                case Languages.ENG:
                    return "Play";
            }
            return "";
        }
    }

    public static string KARAOKE_STATION
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "卡拉OK站";
                case Languages.ENG:
                    return "Karaoke Station";
            }
            return "";
        }
    }

    public static string PENDING
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "請耐心等待...";
                case Languages.ENG:
                    return "Pending...";
            }
            return "";
        }
    }
    public static string PROGRESS
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "處理中： ";
                case Languages.ENG:
                    return "Progress: ";
            }
            return "";
        }
    }
    public static string RENDERINGAUDIO
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "創建音頻文件...";
                case Languages.ENG:
                    return "Rendering audio...";
            }
            return "";
        }
    }


    public static string ENTER_SONG_NAME
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "輸入歌名";
                case Languages.ENG:
                    return "Enter song name:";
            }
            return "";
        }
    }

    public static string SONG_NAME_HERE
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "歌名";
                case Languages.ENG:
                    return "Song name here";
            }
            return "";
        }
    }

    public static string CONTINUE
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "繼續";
                case Languages.ENG:
                    return "Continue";
            }
            return "";
        }
    }

    public static string WARNING_TEXT_LYRICS
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "請不要輸入非中文字";
                case Languages.ENG:
                    return "Found non-Chinese words!";
            }
            return "";
        }
    }

    public static string WARNING_TEXT_SONG_NAME_BLANK
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "請輸入歌名！";
                case Languages.ENG:
                    return "Song name is required!";
            }
            return "";
        }
    }

    public static string WARNING_TEXT_SONG_NAME_INVALID
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "歌名包含無效字符！";
                case Languages.ENG:
                    return "Song name contains invalid characters!";
            }
            return "";
        }
    }

    public static string WARNING_TEXT_SONG_NAME_REPEATED
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "歌名已經被取用！";
                case Languages.ENG:
                    return "Song name already exists!";
            }
            return "";
        }
    }

    public static string ENTER_LYRICS
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return @"輸入歌詞
用空格鍵或輸入新行以分開句子";
                case Languages.ENG:
                    return @"ENTER LYRICS
Use spaces or new lines to separate phrases";
            }
            return "";
        }
    }

    public static string SAVE_SONG_TO_LIBRARY
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "儲存至歌庫";
                case Languages.ENG:
                    return "Save song to library";
            }
            return "";
        }
    }

    public static string DOWNLOAD_MIDI
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "下載 .midi";
                case Languages.ENG:
                    return "Download .midi";
            }
            return "";
        }
    }

    public static string CHANGE_BPM
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "改變歌曲速度";
                case Languages.ENG:
                    return "Change Speed";
            }
            return "";
        }
    }

    public static string PREVIEW_PLAYBACK
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "預覽";
                case Languages.ENG:
                    return "Preview playback";
            }
            return "";
        }
    }

    public static string CHANGING_BPM
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "正在改變速度...";
                case Languages.ENG:
                    return "Changing speed...";
            }
            return "";
        }
    }

    public static string BPM_CHANGE_SUCCESSFUL
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "成功改變速度";
                case Languages.ENG:
                    return "Successfully changed speed";
            }
            return "";
        }
    }

    public static string BPM_CHANGE_FAILED
    {
        get
        {
            switch (language)
            {
                case Languages.CHIN:
                    return "改變速度失敗";
                case Languages.ENG:
                    return "Could not change speed";
            }
            return "";
        }
    }

    
    public TextMeshProUGUI generateText;
    public TextMeshProUGUI libraryText;
    public TextMeshProUGUI karaokeText;
    public TextMeshProUGUI playText;
    public TextMeshProUGUI lyricsHint;
    public TextMeshProUGUI saveSongText;
    public TextMeshProUGUI previewText;
    public TextMeshProUGUI downloadMidiText;
    public TextMeshProUGUI changeTempoText;
    public TextMeshProUGUI continueText;
    public TextMeshProUGUI enterSongNameText;
    public TextMeshProUGUI songNameHint;

    void Start()
    {
        if (generateText != null) generateText.text = GENERATE;
        if (libraryText != null) libraryText.text = LIBRARY;
        if (karaokeText != null) karaokeText.text = KARAOKE_STATION;
        if (playText != null) playText.text = PLAY;
        if (lyricsHint != null) lyricsHint.text = ENTER_LYRICS;
        if (saveSongText != null) saveSongText.text = SAVE_SONG_TO_LIBRARY;
        if (previewText != null) previewText.text = PREVIEW_PLAYBACK;
        if (downloadMidiText != null) downloadMidiText.text = DOWNLOAD_MIDI;
        if (changeTempoText != null) changeTempoText.text = CHANGE_BPM;
        if (continueText != null) continueText.text = CONTINUE;
        if (enterSongNameText != null) enterSongNameText.text = ENTER_SONG_NAME;
        if (songNameHint != null) songNameHint.text = SONG_NAME_HERE;
    }

    void Update()
    {
        Start();
    }

}

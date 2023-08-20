using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    return "等待處理";
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
                    return "處理中：";
                case Languages.ENG:
                    return "Progress";
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
                    return "創建音頻文件";
                case Languages.ENG:
                    return "Rendering audio...";
            }
            return "";
        }
    }


    public static string ENTERSONGNAME
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

    public static string SONGNAMEHERE
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
                    return "請輸入歌名";
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
                    return "歌名包含無效字符";
                case Languages.ENG:
                    return "Song name contains invalid characters!";
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



}

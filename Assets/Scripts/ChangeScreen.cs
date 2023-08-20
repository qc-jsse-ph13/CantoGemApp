using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreen : MonoBehaviour
{
    public void GoToSongNameScreen()
    {
        SceneManager.LoadScene("Song Name Screen", LoadSceneMode.Single);
    }

    public void GoToLyricsScreen()
    {
        if (InputHandler.hasValidSongName)
            SceneManager.LoadScene("Lyrics Screen", LoadSceneMode.Single);
    }

    public void GoToSaveScreen()
    {
        SceneManager.LoadScene("Save Screen", LoadSceneMode.Single);
    }

    public void GoToLoadingScreen()
    {
        if(!InputHandler.containsIllegalCharacters)
            SceneManager.LoadScene("Loading Screen", LoadSceneMode.Single);
    }

    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("Home Screen", LoadSceneMode.Single);
    }

    public void GoToPlaybackScreen()
    {
        SceneManager.LoadScene("Karaoke Playback", LoadSceneMode.Single);
    }

    public void GoToMelodiesScreen()
    {
        SceneManager.LoadScene("All melodies", LoadSceneMode.Single);
    }


    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/qc_jsse");
    }

    public void BuyUsACoffeePlsPlsPls()
    {
        Application.OpenURL("https://www.buymeacoffee.com/jellylab");
    }
}

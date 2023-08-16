using UnityEngine;

// Mediator class
public class SongHolder : MonoBehaviour
{ 
    public static SongHolder Instance { get; private set; }

    public string songName;
    public string lyrics;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

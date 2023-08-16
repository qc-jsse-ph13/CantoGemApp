using System.Collections;
using System.IO;
using UnityEngine;

public class LibraryHandler : MonoBehaviour
{
    public GameObject objectPrefab; 
    public Transform container; 

    private void Start()
    {
        string[] mp3Files = Directory.GetFiles(Application.persistentDataPath, "*.mp3");

        foreach (string filePath in mp3Files)
        {
            GameObject newObj = Instantiate(objectPrefab, container);
        }
    }
}

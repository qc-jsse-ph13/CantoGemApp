using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    public TextMeshProUGUI generateText;
    public TextMeshProUGUI libraryText;

    void Start()
    {
        if (generateText != null) generateText.text = Texts.GENERATE;
        if (libraryText != null) libraryText.text = Texts.LIBRARY;
    }

    void Update()
    {
        Start();
    }
}

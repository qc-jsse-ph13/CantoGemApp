using System.Collections;
using UnityEngine;
using TMPro;

public class KaraokeAnimation : MonoBehaviour
{
    private static int indentSize = 10;

    public TextMeshProUGUI textMeshPro;
    public Gradient colorGradient;

    private static Coroutine animationCoroutine;

    public static void StartAnimation(string text, float[] durations)
    {
        Debug.Log("text animating: " + text);
        instance.StartCoroutine(instance.AnimateKaraoke(text, durations));
    }

    private static KaraokeAnimation instance;

    private void Awake()
    {
        instance = this;
    }

    private string FormatText(string inputText)
    {
        string[] lines = inputText.Split(',');

        string formattedText = lines[0];

        for (int i = 1; i < lines.Length; i++)
        {
            formattedText += $"\n<indent={indentSize}>{lines[i]}</indent>";
        }

        textMeshPro.text = formattedText;
        return formattedText;
    }

    private IEnumerator AnimateKaraoke(string text, float[] letterDurations)
    {
        string[] sections = text.Split(',');

        Color[] originalColors = new Color[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            originalColors[i] = textMeshPro.color;
        }

        int totalVisibleCharacterCount = 0;

        for (int secIndex = 0; secIndex < sections.Length; secIndex++)
        {
            string section = sections[secIndex];
            string formattedSection = FormatText(section);

            for (int i = 0; i < formattedSection.Length; i++)
            {
                float elapsedTime = 0f;
                float letterDuration = letterDurations[totalVisibleCharacterCount];
                totalVisibleCharacterCount++;

                while (elapsedTime < letterDuration)
                {
                    elapsedTime += Time.deltaTime;

                    float t = elapsedTime / letterDuration;
                    Color color = colorGradient.Evaluate(t);

                    string animatedText = formattedSection.Substring(0, i);
                    string animatingText = formattedSection.Substring(i, 1);
                    string unanimatedText = formattedSection.Substring(i + 1);

                    var gradientColour = ColorUtility.ToHtmlStringRGBA(color);
                    var animatedColour = ColorUtility.ToHtmlStringRGBA(colorGradient.Evaluate(1));
                    var unanimatedColour = ColorUtility.ToHtmlStringRGBA(Color.white);

                    textMeshPro.text = "<color=#" + animatedColour + ">" + animatedText +
                                      "<color=#" + gradientColour + ">" + animatingText +
                                      "<color=#" + unanimatedColour + ">" + unanimatedText;
                    textMeshPro.color = originalColors[i];

                    yield return null;
                }
            }
        }

        // Reset text 
        textMeshPro.text = "";
    }
}
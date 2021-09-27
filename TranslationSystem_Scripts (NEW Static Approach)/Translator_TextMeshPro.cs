using TMPro;
using TranslationSystem.Base;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class Translator_TextMeshPro : MonoBehaviour
{
    [SerializeField] private string textKey = "title";
    [SerializeField] private TranslateFormat translateFormat = TranslateFormat.Standard;

    private TextMeshPro textFieldToTranslate;

    private void OnEnable()
    {
        if (textFieldToTranslate == null)
            textFieldToTranslate = GetComponent<TextMeshPro>();

        Translate.OnLanguageChanged += GetTranslatedText;
        GetTranslatedText();
    }

    private void OnDisable()
    {
        Translate.OnLanguageChanged -= GetTranslatedText;
    }

    private void GetTranslatedText()
    {
        textFieldToTranslate.text = Translate.GetTranslatedText(textKey, translateFormat);
    }
}
using TMPro;
using TranslationSystem.Base;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Translator_TextMeshPro : MonoBehaviour
{
    [SerializeField] private string textKey = "title";
    [SerializeField] private TranslateFormat translateFormat = TranslateFormat.Standard;

    private TextMeshProUGUI textFieldToTranslate;

    private void OnEnable()
    {
        if (textFieldToTranslate == null)
            textFieldToTranslate = GetComponent<TextMeshProUGUI>();

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
using TranslationSystem.Components;
using TranslationSystem.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TranslationSystem.Tools
{
    [RequireComponent(typeof(Text))]
    public class Translator_UI_Text : MonoBehaviour
    {
        [Header("Text Config.")] 
        [SerializeField] private Text textFieldToTranslate;

        [Header("Translate Key Config.")] 
        [SerializeField] private string textKey = "title";
        [SerializeField] private TranslateFormat translateFormat = TranslateFormat.Standard;

        private void OnValidate()
        {
            if (textFieldToTranslate == null)
                textFieldToTranslate = GetComponent<Text>();
        }

        private void OnEnable()
        {
            Translate.OnLanguageChanged += GetTranslatedText;
            GetTranslatedText();
        }

        private void OnDisable()
        {
            Translate.OnLanguageChanged -= GetTranslatedText;
        }

        private void GetTranslatedText()
        {
            textFieldToTranslate.text = Translate.GetText(textKey, translateFormat);
        }
    }
}
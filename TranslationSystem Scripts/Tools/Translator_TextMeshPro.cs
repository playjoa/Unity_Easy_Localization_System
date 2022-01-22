using TMPro;
using TranslationSystem.Components;
using TranslationSystem.Controller;
using UnityEngine;

namespace TranslationSystem.Tools
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Translator_TextMeshPro : MonoBehaviour
    {
        [Header("Text Config:")] 
        [SerializeField] private TextMeshProUGUI textFieldToTranslate;

        [Header("Translate Key Config:")] 
        [SerializeField] private string textKey = "title";
        [SerializeField] private TranslateFormat translateFormat = TranslateFormat.Standard;

        private void OnValidate()
        {
            if (textFieldToTranslate == null)
                textFieldToTranslate = GetComponent<TextMeshProUGUI>();
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

        public void SetTranslateText(string translateKey, TranslateFormat format = TranslateFormat.Standard)
        {
            textFieldToTranslate.text = Translate.GetText(translateKey, format);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace TranslationSystem.Components
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Language", fileName = "New Language")]
    public class Language : ScriptableObject
    {
        [SerializeField] private SystemLanguage keyLanguage = SystemLanguage.English;
        [SerializeField] private List<TranslatedText> textsToTranslate = new List<TranslatedText>();

        private Dictionary<string, string> languageDictionary;
        
        public List<TranslatedText> TranslatedTexts => textsToTranslate;
        public SystemLanguage KeyLanguage => keyLanguage;

        public void Initiate() => CreateDictionary();

        public void AddTranslatedText(TranslatedText translatedText)
        {
            textsToTranslate.Add(translatedText);
        }

        private void CreateDictionary()
        {
            languageDictionary = new Dictionary<string, string>();

            foreach (var currentText in textsToTranslate)
                languageDictionary.Add(currentText.GetTextKey(), currentText.GetTranslatedText());
        }

        public string ReturnTranslatedText(string key)
        {
            return languageDictionary.TryGetValue(key, out var valueFound) ? valueFound : $"%{key}%";
        }
    }
}

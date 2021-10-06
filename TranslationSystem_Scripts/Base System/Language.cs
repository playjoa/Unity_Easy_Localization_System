using System.Collections.Generic;
using UnityEngine;

namespace LocalizationSystem
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Language", fileName = "New Language")]
    public class Language : ScriptableObject
    {
        [SerializeField] private SystemLanguage keyLanguage = SystemLanguage.English;
        [SerializeField] private List<TranslatedText> textsToTranslate = new List<TranslatedText>();

        private Dictionary<string, string> languageDictionary;
        public SystemLanguage KeyLanguage => keyLanguage;

        public void SetUpLanguage()
        {
            CreateDictionary();
        }

        private void CreateDictionary()
        {
            languageDictionary = new Dictionary<string, string>();

            foreach (TranslatedText currentText in textsToTranslate)
            {
                languageDictionary.Add(currentText.GetTextKey(), currentText.GetTranslatedText());
            }
        }

        public string ReturnTranslatedText(string key)
        {
            if (languageDictionary.ContainsKey(key)) return languageDictionary[key];
            
            Debug.LogError("Could not find key: " + key);
            return "-empty-";
        }
    }
}

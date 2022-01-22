#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using TranslationSystem.Components;
using TranslationSystem.Controller;
using UnityEditor;
using UnityEngine;

namespace TranslationSystem.EditorTools
{
    public static class TranslateEditorTools
    {
        private const string SyncLanguagesInfo = "Translation Tools/Sync Languages";
        private static SystemLanguage DefaultLanguage => Translate.DefaultLanguage;

        [MenuItem(SyncLanguagesInfo)]
        public static void SyncLanguages()
        {
            var allLanguages = AllLanguagesInResources();
            var defaultLanguage = allLanguages.FirstOrDefault(l => l.KeyLanguage.Equals(DefaultLanguage));

            if (defaultLanguage == null) return;

            allLanguages.Remove(defaultLanguage);

            foreach (var language in allLanguages)
            {
                foreach (var translatedText in defaultLanguage.TranslatedTexts)
                {
                    if (!LanguageHasTextKey(language, translatedText.GetTextKey()))
                        language.AddTranslatedText(translatedText);
                }
            }
        }

        private static bool LanguageHasTextKey(Language language, string textKey)
        {
            return language.TranslatedTexts.Any(text => text.GetTextKey().Equals(textKey));
        }

        private static List<Language> AllLanguagesInResources()
        {
            return Resources.LoadAll<Language>(Translate.LanguagesFolderInResources).ToList();
        }
    }
}
#endif
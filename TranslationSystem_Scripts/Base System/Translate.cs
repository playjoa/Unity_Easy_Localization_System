using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace LocalizationSystem
{
    public static class Translate
    {
        //CHANGE YOUR DESIRED DEFAULT LANGUAGE
        private static SystemLanguage DefaultLanguage => SystemLanguage.English;

        public static event Action OnLanguageChanged;

        private static Dictionary<SystemLanguage, Language> languageDictionary;
        private static SystemLanguage currentLanguage = DefaultLanguage;
        private static Language[] availableLanguages;

        private const string PLAYERPREF_LANGUAGE_KEY = "gameLanguage";
        private const string LANGUAGES_FOLDER_IN_RESOURCES = "Languages";

        public static string CurrentLanguage => currentLanguage.ToString();

        private static void InitializeSystem()
        {
            if (languageDictionary != null)
                return;

            availableLanguages = Resources.LoadAll<Language>(LANGUAGES_FOLDER_IN_RESOURCES);

            CreateDictionary();
            GetCurrentLanguage();
        }

        private static void CreateDictionary()
        {
            languageDictionary = new Dictionary<SystemLanguage, Language>();

            foreach (var language in availableLanguages)
            {
                language.SetUpLanguage();
                languageDictionary.Add(language.KeyLanguage, language);
            }
        }

        private static void GetCurrentLanguage()
        {
            var currentSystemLanguage = CurrentSystemLanguage();

            if (!languageDictionary.ContainsKey(currentSystemLanguage))
            {
                currentLanguage = DefaultLanguage;
                return;
            }

            currentLanguage = currentSystemLanguage;
        }

        public static string GetTranslatedText(string keyText)
        {
            InitializeSystem();

            if (keyText == string.Empty)
                return string.Empty;

            keyText = keyText.ToLower();

            var translatedText = languageDictionary[currentLanguage].ReturnTranslatedText(keyText);

            if (translatedText == "-empty-")
                translatedText = GetDefaultLanguageText(keyText);

            return translatedText;
        }
        
        public static string GetTranslatedText(string keyText, TranslateFormat translateFormat)
        {
            return translateFormat switch
            {
                TranslateFormat.Standard => GetTranslatedText(keyText),
                TranslateFormat.ToUpper => GetTranslatedText(keyText).ToUpper(),
                TranslateFormat.ToLower => GetTranslatedText(keyText).ToLower(),
                TranslateFormat.FirstLetterUpper => GetTranslatedText(keyText).ToTitleCase(),
                _ => GetTranslatedText(keyText)
            };
        }
        
        private static string ToTitleCase(this string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower()); 
        }

        public static void SetNewLanguage(SystemLanguage newLanguage)
        {
            InitializeSystem();

            if (!AvailableLanguages().Contains(newLanguage))
            {
                currentLanguage = DefaultLanguage;
                Debug.LogError("This language [" + newLanguage +
                               "] is not available, switching to default language");
                return;
            }

            currentLanguage = newLanguage;

            RegisterNewLanguageToMemory(currentLanguage);

            OnLanguageChanged?.Invoke();
        }

        public static List<SystemLanguage> AvailableLanguages()
        {
            InitializeSystem();

            return new List<SystemLanguage>(languageDictionary.Keys);
        }

        private static void RegisterNewLanguageToMemory(SystemLanguage newLanguage)
        {
            PlayerPrefs.SetString(PLAYERPREF_LANGUAGE_KEY, newLanguage.ToString());
        }

        private static string GetDefaultLanguageText(string key)
        {
            try
            {
                return languageDictionary[DefaultLanguage].ReturnTranslatedText(key);
            }
            catch
            {
                var returnError = "Default Language not found or set! [" + DefaultLanguage + "]";
                Debug.LogError(returnError);
                return returnError;
            }
        }

        private static SystemLanguage CurrentSystemLanguage()
        {
            if (!PlayerPrefs.HasKey(PLAYERPREF_LANGUAGE_KEY)) return Application.systemLanguage;

            var savedLanguage = PlayerPrefs.GetString(PLAYERPREF_LANGUAGE_KEY);

            return ConvertStringToSystemLanguage(savedLanguage);
        }

        private static SystemLanguage ConvertStringToSystemLanguage(string languageToConvert)
        {
            return (SystemLanguage)Enum.Parse(typeof(SystemLanguage), languageToConvert);
        }
    }
}
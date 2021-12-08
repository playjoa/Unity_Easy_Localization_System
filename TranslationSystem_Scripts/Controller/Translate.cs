using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TranslationSystem.Components;
using UnityEngine;

namespace TranslationSystem.Controller
{
    public static class Translate
    {
        //CHANGE YOUR DESIRED DEFAULT LANGUAGE
        private static SystemLanguage DefaultLanguage => SystemLanguage.English;
        
        private static Dictionary<SystemLanguage, Language> _languagesDictionary;
        private static SystemLanguage _currentLanguage = DefaultLanguage;
        private static Language[] _availableLanguages;
        public static event Action OnLanguageChanged;
        
        private const string PlayerPrefsLanguageKey = "gameLanguage";
        private const string LanguagesFolderInResources = "Languages";

        public static string CurrentLanguage => _currentLanguage.ToString();
        public static List<SystemLanguage> AvailableLanguages => _languagesDictionary.Keys.ToList();
        
        public static void Initiate()
        {
            if (_languagesDictionary != null) return;

            _availableLanguages = Resources.LoadAll<Language>(LanguagesFolderInResources);

            CreateDictionary();
            GetCurrentLanguage();
        }

        private static void CreateDictionary()
        {
            _languagesDictionary = new Dictionary<SystemLanguage, Language>();

            foreach (var language in _availableLanguages)
            {
                language.SetUpLanguage();
                _languagesDictionary.Add(language.KeyLanguage, language);
            }
        }

        private static void GetCurrentLanguage()
        {
            var currentSystemLanguage = CurrentSystemLanguage();
            if (!_languagesDictionary.ContainsKey(currentSystemLanguage))
            {
                _currentLanguage = DefaultLanguage;
                return;
            }
            _currentLanguage = currentSystemLanguage;
        }

        public static string GetText(string keyText)
        {
            Initiate();
            
            if (keyText == string.Empty)
                return string.Empty;

            keyText = keyText.ToLower();

            var translatedText = _languagesDictionary[_currentLanguage].ReturnTranslatedText(keyText);

            if (translatedText.Equals($"%{keyText}%"))
                translatedText = GetDefaultLanguageText(keyText);

            return translatedText;
        }
        
        public static string GetText(string keyText, TranslateFormat translateFormat)
        {
            return translateFormat switch
            {
                TranslateFormat.Standard => GetText(keyText),
                TranslateFormat.ToUpper => GetText(keyText).ToUpper(),
                TranslateFormat.ToLower => GetText(keyText).ToLower(),
                TranslateFormat.FirstLetterUpper => GetText(keyText).ToTitleCase(),
                _ => GetText(keyText)
            };
        }
        
        private static string ToTitleCase(this string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower()); 
        }

        public static void SetNewLanguage(SystemLanguage newLanguage)
        {
            if (!AvailableLanguages.Contains(newLanguage))
            {
                _currentLanguage = DefaultLanguage;
                Debug.LogError($"This language [{newLanguage}] is not available, switching to default language");
                return;
            }

            _currentLanguage = newLanguage;
            RegisterNewLanguageToMemory(_currentLanguage);

            OnLanguageChanged?.Invoke();
        }

        private static void RegisterNewLanguageToMemory(SystemLanguage newLanguage)
        {
            PlayerPrefs.SetString(PlayerPrefsLanguageKey, newLanguage.ToString());
        }

        private static string GetDefaultLanguageText(string key)
        {
            if (_languagesDictionary != null) return _languagesDictionary[DefaultLanguage].ReturnTranslatedText(key);
            Debug.LogError($"Default Language not found or set! [{DefaultLanguage}]");
            return key;
        }

        private static SystemLanguage CurrentSystemLanguage()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsLanguageKey)) return Application.systemLanguage;
            var savedLanguage = PlayerPrefs.GetString(PlayerPrefsLanguageKey);
            return ConvertStringToSystemLanguage(savedLanguage);
        }

        private static SystemLanguage ConvertStringToSystemLanguage(string languageToConvert)
        {
            return (SystemLanguage)Enum.Parse(typeof(SystemLanguage), languageToConvert);
        }
    }
}
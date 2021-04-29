using System;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    [SerializeField]
    private SystemLanguage desiredDefaultLanguage = SystemLanguage.English;

    [SerializeField]
    private List<Language> availableLanguages;
    
    public static event Action OnLanguageChanged;

    private static Dictionary<SystemLanguage, Language> languageDictionary;
    private static SystemLanguage currentLanguage = SystemLanguage.English;
    private static SystemLanguage defaultLanguage = SystemLanguage.English;

    private void Awake()
    {
        InitializeSystem();
    }

    void InitializeSystem()
    {
        defaultLanguage = desiredDefaultLanguage;

        CreateDictionary();
        GetCurrentLanguage();
    }

    void CreateDictionary()
    {
        languageDictionary = new Dictionary<SystemLanguage, Language>();

        foreach (Language currentLanguage in availableLanguages)
        {
            currentLanguage.SetUpLanguage();
            languageDictionary.Add(currentLanguage.KeyLanguage, currentLanguage);
        }
    }

    void GetCurrentLanguage()
    {
        SystemLanguage currentSystemLanguage = Application.systemLanguage;

        if (!languageDictionary.ContainsKey(currentSystemLanguage))
            currentLanguage = defaultLanguage;

        currentLanguage = currentSystemLanguage;
    }

    public static string GetTranslatedText(string keyText)
    {
        keyText = keyText.ToLower();

        if (!languageDictionary.ContainsKey(currentLanguage))
            return GetDefaultLanguageText(keyText);

        string translatedText = languageDictionary[currentLanguage].ReturnTranslatedText(keyText);

        if (translatedText == "-empty-")
            translatedText = GetDefaultLanguageText(keyText);

        return translatedText;
    }

    public static void SetNewLanguage(SystemLanguage newLanguage)
    {
        if (!AvailableLanguages().Contains(newLanguage))
        {
            currentLanguage = defaultLanguage;
            Debug.LogError("This language is not available, switching to default language");
            return;
        }
        
        currentLanguage = newLanguage;

        OnLanguageChanged();
    }

    public static List<SystemLanguage> AvailableLanguages()
    { 
        return  new List<SystemLanguage>(languageDictionary.Keys);
    }

    static string GetDefaultLanguageText(string key)
    {
        try
        {
            return languageDictionary[defaultLanguage].ReturnTranslatedText(key);
        }
        catch
        {
            string returnError = "Default Language not found or set! [" + defaultLanguage + "]";
            Debug.LogError(returnError);
            return returnError;
        }
    } 
}


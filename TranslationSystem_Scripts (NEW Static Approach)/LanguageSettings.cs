using System.Collections.Generic;
using TranslationSystem.Base;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSettings : MonoBehaviour
{
    [SerializeField] private Dropdown dropDownLanguageOptions;

    private void OnEnable()
    {
        SetUpLanguageOptions();
    }

    private void SetUpLanguageOptions()
    {
        var languagesNames = new List<string>();
        var idCurrentLanguage = 0;


        dropDownLanguageOptions.ClearOptions();

        for (var i = 0; i < Translate.AvailableLanguages().Count; i++)
        {
            languagesNames.Add(Translate.AvailableLanguages()[i].ToString());

            if (isLanguageCurrentLanguage(Translate.AvailableLanguages()[i]))
                idCurrentLanguage = i;

        }

        dropDownLanguageOptions.AddOptions(languagesNames);

        dropDownLanguageOptions.value = idCurrentLanguage;
        dropDownLanguageOptions.RefreshShownValue();
    }
    
    private bool isLanguageCurrentLanguage(SystemLanguage language)
    {
        return language.ToString() == Translate.CurrentLanguage;
    }

    public void SetLanguage(int idDesiredLanguage)
    {
        var languageToSet = Translate.AvailableLanguages()[idDesiredLanguage];

        Translate.SetNewLanguage(languageToSet);
    }
}

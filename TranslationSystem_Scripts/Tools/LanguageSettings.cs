using System.Collections.Generic;
using TranslationSystem.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TranslationSystem.Tools
{
    public class LanguageSettings : MonoBehaviour
    {
        [SerializeField] private Dropdown dropDownLanguageOptions;

        private void OnEnable()
        {
            SetUpLanguageOptions();
        }

        private void SetUpLanguageOptions()
        {
            var allAvailableLanguages = Translate.AvailableLanguages;
            var languagesNames = new List<string>();
            var idCurrentLanguage = 0;

            dropDownLanguageOptions.ClearOptions();

            for (var i = 0; i < allAvailableLanguages.Count; i++)
            {
                languagesNames.Add(allAvailableLanguages[i].ToString());
                if (CurrentLanguage(allAvailableLanguages[i]))
                    idCurrentLanguage = i;
            }

            dropDownLanguageOptions.AddOptions(languagesNames);
            dropDownLanguageOptions.value = idCurrentLanguage;
            dropDownLanguageOptions.RefreshShownValue();
        }

        private bool CurrentLanguage(SystemLanguage language) => language.ToString().Equals(Translate.CurrentLanguage);

        public void SetLanguage(int idDesiredLanguage)
        {
            var languageToSet = Translate.AvailableLanguages[idDesiredLanguage];
            Translate.SetNewLanguage(languageToSet);
        }
    }
}
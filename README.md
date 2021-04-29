# Unity_LocalizationSystem

Easy way to implement localization for your games! Translate to any language! This approach doesn't use text files, you can create your dictionary inside Unity!

## Features:
- Easy to use.
- Ready for changing languages in game.
- Add multiple languages.
- OnLanguageChange call to update active texts.

## How to setup:

- First you'll need to place the script in an object thats located in the <strong>first scene</strong> of your game! There's a prefab included for this if you want.

### Create a language
![1](Screenshots/0.png)

- Create a language in your project.

### SetUp your default language
![1](Screenshots/1.png)

- Set up it's texts along with the keys that will be used to track translations.

### Create other languages
![1](Screenshots/2.png)

- Select the desired language.
- Copy the keys from your default language and update the texts to match the new language.

### Assing the languages to the translator
![1](Screenshots/3.png)

- Select the desired default language. (It will be used if a key is not found in another language or if the translator doesn't have the system language)
- Assign the languages to the translator

### Using Translator
![1](Screenshots/4.png)

- Attatch the Translator_UI_Text to a text object and write down the desired key.

### Done
![1](Screenshots/5.png)


### OBS:

- Unity might be weird sometimes and execute some "OnEnable"s before some "Awakes".
- This can cause errors if you want to translate Texts in the <strong>first scene</strong> of your project.
- But this can be easily fixed.

### How to fix:

![1](Screenshots/5.png)
- Go to "Project Settings/Script Execution Order" and place the translate script with a -1. That will fix your problems :D



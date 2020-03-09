using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Events;
using System.Globalization;

public class LanguageManager : MonoBehaviour {

    public static LanguageManager instance;

    private Data data;

    private LanguageData currentLanguageData;
    private LanguageData defaultLanguageData;

    public UnityEvent onLanguageChange;

    private void Awake()
    {
        instance = this;

        //Read JSON file
        string text = File.ReadAllText(Application.dataPath + "\\localization.json", System.Text.Encoding.UTF8);
        Debug.Log(text);

        data = JsonUtility.FromJson<Data>(text);

        defaultLanguageData = GetLanguageData("English");
        SelectLanguage("English", false);

        onLanguageChange = new UnityEvent();
    }

    public void SelectLanguage(string language, bool updateUI = true)
    {
        if (language == "none")
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;
            currentLanguageData = GetLanguageDataISO639_1(ci.TwoLetterISOLanguageName);

            if (currentLanguageData == null)
            {
                Debug.Log("Language not supported: "+ci.TwoLetterISOLanguageName);
                currentLanguageData = defaultLanguageData;
            }
        } else
        {
            currentLanguageData = GetLanguageData(language);
        }

        if (updateUI) onLanguageChange.Invoke();
    }

    private LanguageData GetLanguageData(string language)
    {
        return data.data.Find(x => x.language == language);
    }
    private LanguageData GetLanguageDataISO639_1(string iso639_1)
    {
        return data.data.Find(x => x.iso_639_1 == iso639_1);
    }

    public List<string> GetLanguages()
    {
        List<string> languages = new List<string>();

        foreach (LanguageData languageData in data.data)
        {
            languages.Add(languageData.language);
        }

        return languages;
    }

    public string GetCurrentLanguage()
    {
        return currentLanguageData.language;
    }

    public string GetTranslation(string id)
    {
        LanguageDataKeyValue keyValue = currentLanguageData.key_values.Find(x => x.key == id);

        if (keyValue != null)
        {
            return keyValue.value;
        } else
        {
            Debug.LogWarning("No entry found for " + id);
            keyValue = defaultLanguageData.key_values.Find(x => x.key == id);

            if (keyValue != null)
            {
                return keyValue.value;
            } else
            {
                return "null";
            }
        }
    }



    [System.Serializable]
    private class Data
    {
        public List<LanguageData> data;
    }

    [System.Serializable]
    private class LanguageData {
        public string language;
        public string iso_639_1;
        public List<LanguageDataKeyValue> key_values;
    }

    [System.Serializable]
    private class LanguageDataKeyValue
    {
        public string key;
        public string value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILanguageButton : MonoBehaviour {

    private bool bSelected = false;

    private List<string> languages;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (bSelected && UIWindowDropdown.instance.IsDone())
        {
            int selectedIndex = UIWindowDropdown.instance.GetIndex();
            PlayBounds_Prefs_Handler.instance.language = languages[selectedIndex];
            bSelected = false;
        }
    }

    public void OnClick()
    {
        languages = LanguageManager.instance.GetLanguages();
        UIWindowDropdown.instance.Show("Language", languages.ToArray(), languages.IndexOf(LanguageManager.instance.GetCurrentLanguage()));
        bSelected = true;
    }
}

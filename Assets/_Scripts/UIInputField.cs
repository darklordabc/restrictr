using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using Valve.VR;
using System.Text;

public class UIInputField : MonoBehaviour {

    private bool bSelected = false;

    public string textName;
    public Text text;

    private PlayBounds_Menu_Redux menu;


    // Use this for initialization
    void Start() {
        menu = GetComponentInParent<PlayBounds_Menu_Redux>();
        text.text = PlayBounds_Prefs_Handler.instance.GetText(menu.activeTab == PlayBounds_Menu_Redux.ActiveTab.Global, textName);
    }

    // Update is called once per frame
    void Update() {
        if (bSelected && UIWindowInputField.instance.IsDone()) {
            PlayBounds_Prefs_Handler.instance.SetText(menu.activeTab == PlayBounds_Menu_Redux.ActiveTab.Global, textName, UIWindowInputField.instance.GetText());
            text.text = PlayBounds_Prefs_Handler.instance.GetText(menu.activeTab == PlayBounds_Menu_Redux.ActiveTab.Global, textName);
            bSelected = false;
        }
    }

    public void OnClick() {
        UIWindowInputField.instance.Show(textName, text.text);
        bSelected = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using System.Text;

public class UIWindowInputField : MonoBehaviour {

    public static UIWindowInputField instance;

    public GameObject goWindow;
    public Text label;
    public InputField inputField;
    public bool bTypingInWindow = false;
    public bool bTypingInVR = false;

    private void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        try {
            StringBuilder sb = new StringBuilder(256);
            OpenVR.Overlay.GetKeyboardText(sb, 256);
            if (bTypingInWindow || Input.anyKeyDown) {
                bTypingInWindow = true;
            } else if (bTypingInVR || !inputField.text.Equals(sb.ToString())) {
                inputField.text = sb.ToString();
            }

            //PlayBounds_Prefs_Handler.instance.SetText(menu.activeTab == PlayBounds_Menu_Redux.ActiveTab.Global, textName, text.text);
        } catch (System.Exception e) {

        }

    }

    public void Show(string label, string value) {
        this.label.text = label;
        this.inputField.text = value;
        goWindow.SetActive(true);
        this.inputField.ActivateInputField();

        ShowKeyboard();
        bTypingInVR = false;
        bTypingInWindow = false;
    }

    public void Hide() {
        goWindow.SetActive(false);
        HideKeyboard();
    }

    public void OnDisable() {
        HideKeyboard();
    }

    public string GetText() {
        return inputField.text;
    }

    public bool IsDone() {
        return !goWindow.activeSelf;
    }

    public void ShowKeyboard() {
        OpenVR.Overlay.ShowKeyboardForOverlay(0, 0, 0, label.text, 256, inputField.text, false, 0);
    }

    public void HideKeyboard() {
        OpenVR.Overlay.HideKeyboard();
    }
}

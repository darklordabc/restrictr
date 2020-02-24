using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class UIWindowDropdown : MonoBehaviour {

    public static UIWindowDropdown instance;

    public GameObject goWindow;
    public Text txtTitle;
    public Button[] buttons;
    public Button btnPrev;
    public Button btnNext;

    private string[] elements;
    private int listIndex;
    private int selectedIndex;

    private void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (!goWindow.activeSelf) return;

        for (int i = 0; i < buttons.Length; i++) {
            if (i+listIndex < elements.Length) {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<Text>().text = elements[i + listIndex];
                buttons[i].interactable = (selectedIndex != i + listIndex);
            } else {
                buttons[i].gameObject.SetActive(false);
            }
        }

        btnPrev.gameObject.SetActive(CanSelectPrev());
        btnNext.gameObject.SetActive(CanSelectNext());
    }

    public void Show(string label, string[] values, int selectedIndex) {
        goWindow.SetActive(true);
        txtTitle.text = label;
        elements = values;
        listIndex = 0;
        this.selectedIndex = selectedIndex;
    }

    public int GetIndex() {
        return selectedIndex;
    }

    public bool IsDone() {
        return !goWindow.activeSelf;
    }

    public void Next() {
        if (CanSelectNext()) {
            listIndex += 10;
        }
    }

    public void SelectIndex(int index) {
        selectedIndex = listIndex + index;
    }

    public void Prev() {
        if (CanSelectPrev()) {
            listIndex -= 10;
        }
    }

    private bool CanSelectPrev() {
        return listIndex >= 10;
    }

    private bool CanSelectNext() {
        return listIndex + 10 < elements.Length;
    }
}

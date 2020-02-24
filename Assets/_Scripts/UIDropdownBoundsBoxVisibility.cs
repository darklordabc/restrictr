using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDropdownBoundsBoxVisibility : MonoBehaviour {

    private bool bSelected = false;

    public Text label;
    public Text text;

    public string[] elements;
    public int selectedIndex;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (bSelected && UIWindowDropdown.instance.IsDone()) {
            selectedIndex = UIWindowDropdown.instance.GetIndex();
            GetComponentInParent<PlayBounds_Menu_Redux>().SetBoundsBoxVisibility();
            bSelected = false;
        }
        if (elements.Length > 0) {
            selectedIndex = Mathf.Clamp(selectedIndex, 0, elements.Length);
            text.text = elements[selectedIndex];
        }
    }

    public void OnClick() {
        UIWindowDropdown.instance.Show(label.text, elements, selectedIndex);
            bSelected = true;
    }
}

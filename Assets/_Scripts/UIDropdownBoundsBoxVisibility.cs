using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDropdownBoundsBoxVisibility : MonoBehaviour {

    private bool bSelected = false;

    public Text label;
    public Text text;

    public BufferLocalizer[] elements;
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
            text.text = elements[selectedIndex].localizedValue;
        }
    }

    public void OnClick() {
        List<string> elementStrings = new List<string>();

        foreach (BufferLocalizer b in elements)
        {
            elementStrings.Add(b.localizedValue);
        }

        UIWindowDropdown.instance.Show(label.text, elementStrings.ToArray(), selectedIndex);
            bSelected = true;
    }
}

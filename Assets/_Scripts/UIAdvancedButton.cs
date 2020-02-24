using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAdvancedButton : MonoBehaviour {

    public GameObject advancedOptions;
    public Text text;

    private bool bShowAdvanced = false;

	// Use this for initialization
	void Start () {
        bShowAdvanced = false;
        advancedOptions.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!bShowAdvanced) text.text = "Show advanced";
        else text.text = "Hide advanced";
	}

    public void OnClick() {
        bShowAdvanced = !bShowAdvanced;

        advancedOptions.SetActive(bShowAdvanced);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAdvancedButton : MonoBehaviour {

    public GameObject advancedOptions;
    public Text text;
    public BufferLocalizer bufferLocalizerShowAdvanced;
    public BufferLocalizer bufferLocalizerHideAdvanced;

    private bool bShowAdvanced = false;

	// Use this for initialization
	void Start () {
        bShowAdvanced = false;
        advancedOptions.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!bShowAdvanced) text.text = bufferLocalizerShowAdvanced.localizedValue;
        else text.text = bufferLocalizerHideAdvanced.localizedValue;
	}

    public void OnClick() {
        bShowAdvanced = !bShowAdvanced;

        advancedOptions.SetActive(bShowAdvanced);
    }
}

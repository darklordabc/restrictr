using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLocalizer : BufferLocalizer
{
    private Text text;
    private void Awake()
    {
        text = GetComponent<Text>();

        //Auto-size text
        text.resizeTextMinSize = 0;
        text.resizeTextMaxSize = text.fontSize;
        text.resizeTextForBestFit = true;
    }

    public override void UpdateText()
    {
        base.UpdateText();
        text.text = localizedValue;
    }
}

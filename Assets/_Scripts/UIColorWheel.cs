using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorWheel : MonoBehaviour {

    public UIGradient uiGradientValue;
    public UIGradient uiGradientSaturation;
    public Slider sliderSaturation;
    public Slider sliderValue;
    public UIColorHue uiColorHue;

    private float hue;
    private float saturation;
    private float value;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        hue = uiColorHue.GetValue();
        saturation = sliderSaturation.value;
        value = sliderValue.value;

        uiGradientValue.m_color1 = Color.HSVToRGB(hue, saturation, 0f);
        uiGradientValue.m_color2 = Color.HSVToRGB(hue, saturation, 1f);
        //uiGradientValue.
        uiGradientSaturation.m_color1 = Color.HSVToRGB(hue, 0f, value);
        uiGradientSaturation.m_color2 = Color.HSVToRGB(hue, 1f, value);

        PlayBounds_Prefs_Handler.instance.SetColor(Color.HSVToRGB(hue, saturation, value));
    }

    private void OnEnable() {
        Color color = PlayBounds_Prefs_Handler.instance.GetColor();
        Color.RGBToHSV(color, out hue, out saturation, out value);

        sliderSaturation.value = saturation;
        sliderValue.value = value;
        uiColorHue.SetValue(hue);
    }
}

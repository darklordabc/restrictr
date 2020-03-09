using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using Valve.VR;
using System.Text;

public class UISliderInputField : MonoBehaviour {

    private bool bSelected = false;

    private Slider slider;
    private PlayBounds_Menu_Redux menu;

    //public string textName;

    public BufferLocalizer bufferLocalizer;


    // Use this for initialization
    void Start() {
        menu = GetComponentInParent<PlayBounds_Menu_Redux>();
        slider = GetComponentInParent<Slider>();
    }

    // Update is called once per frame
    void Update() {
        if (bSelected && UIWindowInputField.instance.IsDone()) {
            float value;
            if (float.TryParse(UIWindowInputField.instance.GetText(), out value)) {
                slider.value = value;
                bSelected = false;
            }
        }
    }

    public void OnClick() {
        UIWindowInputField.instance.Show(bufferLocalizer.localizedValue, slider.value.ToString("F2"));
        bSelected = true;
    }
}

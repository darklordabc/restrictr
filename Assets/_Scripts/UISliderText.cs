using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UISliderText : MonoBehaviour {

    public Text text;
    public Slider slider;

    public string textPrev;
    public string textPost;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = textPrev + slider.value.ToString("F2") + textPost;
    }
}

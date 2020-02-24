using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderControls : MonoBehaviour {

    private Slider slider;

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Prev() {
        slider.value -= 0.01f;
    }
    public void PrevPlus() {
        slider.value -= 0.1f;
    }
    public void Next() {
        slider.value += 0.01f;
    }
    public void NextPlus() {
        slider.value += 0.1f;
    }
}

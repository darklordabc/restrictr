using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIColor : MonoBehaviour {

    public Image image;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        image.color = PlayBounds_Prefs_Handler.instance.GetColor();
    }
}

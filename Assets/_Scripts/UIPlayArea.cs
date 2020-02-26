using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class UIPlayArea : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        float sizeX = 0f;
        float sizeZ = 0f;

        text.text = "Play area = ";

        try
        {
            if (OpenVR.ChaperoneSetup.GetWorkingPlayAreaSize(ref sizeX, ref sizeZ))
            {
                text.text += sizeX.ToString("F2") + "m x " + sizeZ.ToString("F2") + "m";
            }
            else
            {
                text.text += "unknown";
            }
        } catch
        {
            text.text += "unknown";
        }
	}
}

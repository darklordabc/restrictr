using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReturnText : MonoBehaviour {

    private TextMeshPro tmp;
    private Color cFull;
    private Color cClear;

	// Use this for initialization
	void Start () {
        tmp = GetComponent<TextMeshPro>();
        cFull = tmp.color;
        cClear = new Color(cFull.r, cFull.g, cFull.b, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        tmp.color = Color.Lerp(cClear, cFull, Mathf.PingPong(Time.realtimeSinceStartup / PlayBounds_Prefs_Handler.instance.GetTimeTextFlash(),1f));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsCurrentGame : MonoBehaviour {

    private Text text;

    public BufferLocalizer bufferLocalizerCurrentGame;
    public BufferLocalizer bufferLocalizerNone;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        string gameName = PlayBoundsManager.instance.GetRunningAppIdName();
        if (gameName != null) {
            text.text = bufferLocalizerCurrentGame.localizedValue + ": <color=red>" + gameName + "</color>";
        } else {
            text.text = bufferLocalizerCurrentGame.localizedValue + ": <color=red>" + bufferLocalizerNone.localizedValue + "</color>";
        }
	}
}

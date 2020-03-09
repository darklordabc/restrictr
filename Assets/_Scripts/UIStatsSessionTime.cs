using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsSessionTime : MonoBehaviour {

    private Text text;

    public BufferLocalizer bufferLocalizerSessionTime;
    public BufferLocalizer bufferLocalizerSessionTimeHours;
    public BufferLocalizer bufferLocalizerSessionTimeMinutes;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        float gameTime = PlayBoundsManager.instance.GetRunningAppTime();
        if (gameTime > 0f) {
            text.text = bufferLocalizerSessionTime.localizedValue + ": <color=red>" 
                + Mathf.FloorToInt(gameTime / 3600f) + " " + bufferLocalizerSessionTimeHours.localizedValue 
                + ", " + Mathf.FloorToInt(gameTime / 60f) % 60 + " "+ bufferLocalizerSessionTimeMinutes.localizedValue + "</color>";
        } else {
            text.text = "";
        }
    }
}

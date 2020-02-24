using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsSessionTime : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        float gameTime = PlayBoundsManager.instance.GetRunningAppTime();
        if (gameTime > 0f) {
            text.text = "Session time: <color=red>" + Mathf.FloorToInt(gameTime / 3600f) + " hours, " + Mathf.FloorToInt(gameTime / 60f) % 60 + " minutes</color>";
        } else {
            text.text = "";
        }
    }
}

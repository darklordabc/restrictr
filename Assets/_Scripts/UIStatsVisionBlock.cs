using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsVisionBlock : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayBoundsManager.instance.IsRunningAnApp()) {
            text.text = "Vision block activated: <color=red>" + PlayBoundsManager.instance.GetRunningAppVisionBlockActivatedTimes() + " times</color>";
        } else {
            text.text = "";
        }
    }
}

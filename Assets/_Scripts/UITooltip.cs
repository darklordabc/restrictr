using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITooltip : MonoBehaviour {

    public static UITooltip instance;

    public GameObject goObject;
    public RectTransform rectBackground;
    public Text text;

    private string tooltipText;

	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        rectBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, text.preferredWidth+12);

        text.text = tooltipText;
        goObject.SetActive(tooltipText != "");

        tooltipText = "";
	}

    public void ShowTooltip(string text, Vector2 mousePosition) {
        tooltipText = text;

        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), mousePosition, Camera.main, out localPoint)) {
            goObject.transform.localPosition = localPoint;
        }

    }
}
